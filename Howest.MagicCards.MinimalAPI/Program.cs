global using AutoMapper;
global using Howest.MagicCards.DAL.Contexts;
global using Howest.MagicCards.DAL.Entities;
global using Howest.MagicCards.DAL.Repositories.Implementations;
global using Howest.MagicCards.DAL.Repositories.Interfaces;
global using Howest.MagicCards.Shared.Dtos;
global using Howest.MagicCards.Shared.Mapping;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddScoped<ICardRepository, CardRepository>();
    services.AddScoped<IDeckRepository, DeckRepository>();
    services.AddAutoMapper(typeof(MappingProfile));
    services.AddDbContext<MagicCardsDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("MagicCardsDb")));
}

void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapGet("/", () => "Magic The Gathering");

    app.MapPost("/deck/add", AddCardToDeck);
    app.MapDelete("/deck/{cardId}", DeleteCardFromDeck);
    app.MapGet("/deck", GetDeck);
    app.MapPost("/deck/clear", ClearDeck);
}

async Task<IResult> AddCardToDeck(CardDetailDto cardDto, [FromServices] IDeckRepository deckRepository, [FromServices] IMapper mapper)
{
    Card card = mapper.Map<Card>(cardDto);
    await deckRepository.AddCardToDeckAsync(card);
    return Results.Created($"/deck", cardDto);
}

async Task<IResult> DeleteCardFromDeck(long cardId, [FromServices] IDeckRepository deckRepository)
{
    await deckRepository.DeleteCardFromDeckAsync(cardId);
    return Results.Accepted($"/deck/{cardId}");
}

async Task<IResult> GetDeck([FromServices] IDeckRepository deckRepository, [FromServices] IMapper mapper)
{
    List<Card> deck = await deckRepository.GetDeckAsync();
    List<CardDetailDto> deckDto = mapper.Map<List<CardDetailDto>>(deck);
    return Results.Ok(deckDto);
}

async Task<IResult> ClearDeck([FromServices] IDeckRepository deckRepository)
{
    await deckRepository.ClearDeckAsync();
    return Results.Ok();
}
