
using System.Collections.Specialized;
using Howest.MagicCards.Shared.Dtos;
using System.Web;

namespace Howest.MagicCards.Web.Services;

public class CardService
{
    private readonly HttpClient _http;

    public CardService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CardListDto>> GetCardsAsync(CardFilterDto filter, int pageSize = 150, int pageNumber = 1, string sortBy = "Name", bool sortDescending = false)
    {
        UriBuilder builder = new UriBuilder($"{_http.BaseAddress}api/1.5/Cards");
        NameValueCollection queryParams = HttpUtility.ParseQueryString(builder.Query);

        queryParams["pageSize"] = pageSize.ToString();
        queryParams["pageNumber"] = pageNumber.ToString();
        queryParams["sortBy"] = sortBy;
        queryParams["sortDescending"] = sortDescending.ToString();

        AddFilterToQueryParams(filter, queryParams);

        builder.Query = queryParams.ToString();

        HttpResponseMessage response = await _http.GetAsync(builder.ToString());

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<CardListDto>>();
    }

    public async Task<CardDetailDto> GetCardDetailAsync(long id)
    {
        string finalUrl = $"{_http.BaseAddress}api/1.5/Cards/{id}";

        HttpResponseMessage response = await _http.GetAsync(finalUrl);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CardDetailDto>();
    }

    private void AddFilterToQueryParams(CardFilterDto filter, NameValueCollection queryParams)
    {
        if (!string.IsNullOrEmpty(filter.SetName))
            queryParams["Set"] = filter.SetName;
        if (!string.IsNullOrEmpty(filter.ArtistName))
            queryParams["Artist"] = filter.ArtistName;
        if (!string.IsNullOrEmpty(filter.RarityName))
            queryParams["Rarity"] = filter.RarityName;
        if (!string.IsNullOrEmpty(filter.CardTypeShortHand))
            queryParams["CardType"] = filter.CardTypeShortHand;
        if (!string.IsNullOrEmpty(filter.CardName))
            queryParams["CardName"] = filter.CardName;
        if (!string.IsNullOrEmpty(filter.CardText))
            queryParams["CardText"] = filter.CardText;
    }
}
