
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.DAL.Repositories.Interfaces;

public interface IArtistRepository
{
    Task<IEnumerable<Artist>> GetArtistsAsync();
    Task<Artist> GetArtistByIdAsync(long id);
}
