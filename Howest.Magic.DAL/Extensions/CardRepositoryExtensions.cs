
using Howest.MagicCards.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Extensions;

public static class CardRepositoryExtensions
{
    public static IQueryable<Card> IncludeStandardNavigation(this IQueryable<Card> query)
    {
        return query.Include(c => c.SetCodeNavigation)
                    .Include(c => c.Artist)
                    .Include(c => c.RarityCodeNavigation)
                    .Include(c => c.CardColors).ThenInclude(cc => cc.Color)
                    .Include(c => c.CardTypes).ThenInclude(ct => ct.Type);
    }
}
