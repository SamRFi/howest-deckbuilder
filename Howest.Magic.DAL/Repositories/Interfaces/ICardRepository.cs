
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.DAL.Repositories.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<Card>> GetCardsAsync(CardFilterRecord filter, int pageSize, int pageNumber, string sortBy, bool sortDescending);
    Task<Card> GetCardDetailsAsync(int id);

    Task<IEnumerable<Card>> GetAllCardsAsync();

    Task<Card> GetCardByIdAsync(long id);

    Task<IEnumerable<Card>> GetCardsByArtistIdAsync(long artistId);
}
