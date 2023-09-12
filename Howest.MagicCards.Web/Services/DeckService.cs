
using Howest.MagicCards.Shared.Dtos;

namespace Howest.MagicCards.Web.Services;

public class DeckService
{
    private readonly HttpClient _http;

    public DeckService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<DeckCardDto>> GetDeckAsync()
    {
        HttpResponseMessage response = await _http.GetAsync("Deck");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get deck. Response status code: {response.StatusCode}");
        }
        return await response.Content.ReadFromJsonAsync<List<DeckCardDto>>();
    }

    public async Task AddCardToDeckAsync(CardDetailDto card)
    {
        HttpResponseMessage response = await _http.PostAsJsonAsync("Deck/add", card);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to add card to deck. Response status code: {response.StatusCode}");
        }
    }

    public async Task DeleteCardFromDeckAsync(long id)
    {
        HttpResponseMessage response = await _http.DeleteAsync($"Deck/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to delete card from deck. Response status code: {response.StatusCode}");
        }
    }

    public async Task ClearDeckAsync()
    {
        HttpResponseMessage response = await _http.PostAsync("Deck/clear", null);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to clear deck. Response status code: {response.StatusCode}");
        }
    }
}
