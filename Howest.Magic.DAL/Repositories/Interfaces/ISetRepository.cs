
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.DAL.Repositories.Interfaces
{
    internal interface ISetRepository
    {
        Task<IEnumerable<Set>> GetSetsAsync();
    }
}
