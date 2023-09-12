using System.Reflection;
using AutoMapper;
using Howest.MagicCards.DAL;
using Howest.MagicCards.DAL.Entities;
using Howest.MagicCards.DAL.Repositories.Interfaces;
using Howest.MagicCards.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Howest.MagicCards.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.1")]
[ApiVersion("1.5")]
[Route("api/{v:apiVersion}/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly PagingOptions _pagingOptions;

    public CardsController(ICardRepository cardRepository, IMapper mapper, IMemoryCache cache, IOptions<PagingOptions> pagingOptions)
    {
        _cardRepository = cardRepository;
        _mapper = mapper;
        _cache = cache;
        _pagingOptions = pagingOptions.Value;
    }

    private string GenerateCacheKey(CardFilterRecord filter, int pageSize, int pageNumber, string sortBy, bool sortDescending = false)
    {
        PropertyInfo[] filterProperties = filter.GetType().GetProperties();
        IEnumerable<string> filterValues = filterProperties.Select(p => $"{p.Name}:{p.GetValue(filter)}");
        string cacheKey = string.Join("-", filterValues) + $"-{pageSize}-{pageNumber}-{sortBy}-{sortDescending}";
        return cacheKey;
    }


    // GET: api/v1.1/Cards
    [HttpGet]
    [MapToApiVersion("1.1")]
    public async Task<ActionResult<IEnumerable<CardListDto>>> GetCardsV1_1([FromQuery] CardFilterRecord filter, [FromQuery] int pageSize = 150, [FromQuery] int pageNumber = 1, [FromQuery] string sortBy = "Name")
    {
        try
        {
            if (pageSize > _pagingOptions.MaxPageSize)
            {
                pageSize = _pagingOptions.MaxPageSize;
            }

            string cacheKey = GenerateCacheKey(filter, pageSize, pageNumber, sortBy);

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Card> cards))
            {
                cards = await _cardRepository.GetCardsAsync(filter, pageSize, pageNumber, sortBy, false);
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(cacheKey, cards, cacheEntryOptions);
            }
            return Ok(_mapper.Map<IEnumerable<CardListDto>>(cards));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    // GET: api/v1.5/Cards
    [HttpGet]
    [MapToApiVersion("1.5")]
    public async Task<ActionResult<IEnumerable<CardListDto>>> GetCardsV1_5([FromQuery] CardFilterRecord filter, [FromQuery] int pageSize = 150, [FromQuery] int pageNumber = 1, [FromQuery] string sortBy = "Name", [FromQuery] bool sortDescending = false)
    {
        string cacheKey = GenerateCacheKey(filter, pageSize, pageNumber, sortBy, sortDescending);
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Card> cards))
        {
            if (pageSize > _pagingOptions.MaxPageSize)
            {
                pageSize = _pagingOptions.MaxPageSize;
            }
            cards = await _cardRepository.GetCardsAsync(filter, pageSize, pageNumber, sortBy, sortDescending);
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, cards, cacheEntryOptions);
        }
        return Ok(_mapper.Map<IEnumerable<CardListDto>>(cards));
    }

    // GET: api/v1.5/Cards/5
    [HttpGet("{id}")]
    [MapToApiVersion("1.5")]
    public async Task<ActionResult<CardDetailDto>> GetCard(int id)
    {
        string cacheKey = $"Card-{id}";
        if (!_cache.TryGetValue(cacheKey, out Card card))
        {
            card = await _cardRepository.GetCardDetailsAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, card, cacheEntryOptions);
        }
        return Ok(_mapper.Map<CardDetailDto>(card));
    }




}
