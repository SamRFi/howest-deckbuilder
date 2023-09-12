
using System.Text.Json;
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.DAL.Extensions;

public static class DeckRepositoryExtensions
{
    public static async Task WriteDeckToJsonAsync(this List<Card> deck, string filePath)
    {
        var jsonString = JsonSerializer.Serialize(deck);
        await File.WriteAllTextAsync(filePath, jsonString);
    }
}
