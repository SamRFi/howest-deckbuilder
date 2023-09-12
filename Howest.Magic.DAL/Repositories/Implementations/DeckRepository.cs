
using System.Reflection;
using System.Text.Json;
using Howest.MagicCards.DAL.Entities;
using Howest.MagicCards.DAL.Extensions;
using Howest.MagicCards.DAL.Repositories.Interfaces;

namespace Howest.MagicCards.DAL.Repositories.Implementations
{
    public record DeckRepository : IDeckRepository
    {
        private readonly string _filePath;

        public DeckRepository()
        {
            _filePath = Path.Combine(GetProjectRoot(), "deck.json");
        }

        public async Task AddCardToDeckAsync(Card card)
        {
            List<Card> deck = await ReadDeckFromJsonAsync();

            deck.Add(card);

            await deck.WriteDeckToJsonAsync(_filePath);
        }

        public async Task DeleteCardFromDeckAsync(long cardId)
        {
            List<Card> deck = await ReadDeckFromJsonAsync();

            deck.RemoveAll(c => c.Id == cardId);

            await deck.WriteDeckToJsonAsync(_filePath);
        }

        public Task<List<Card>> GetDeckAsync() => ReadDeckFromJsonAsync();

        public async Task<List<Card>> ReadDeckFromJsonAsync()
        {
            return File.Exists(_filePath)
                ? JsonSerializer.Deserialize<List<Card>>(await File.ReadAllTextAsync(_filePath))
                : new List<Card>();
        }

        public async Task ClearDeckAsync()
        {
            await new List<Card>().WriteDeckToJsonAsync(_filePath);
        }

        private static string GetProjectRoot()
            => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\..\"));


    }
}
