
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.DAL.Repositories.Interfaces;

internal interface IRarityRepository
{
    Task<IEnumerable<Rarity>> GetRaritiesAsync();
}
