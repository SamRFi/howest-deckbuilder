
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.Shared.Extensions;

public static class ArtistRepositoryExtensions
{
    public static IQueryable<Artist> WithId(this IQueryable<Artist> query, long id)
    {
        return query.Where(a => a.Id == id);
    }
}
