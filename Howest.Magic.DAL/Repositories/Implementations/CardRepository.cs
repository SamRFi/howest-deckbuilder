
using Howest.MagicCards.DAL.Contexts;
using Howest.MagicCards.DAL.Entities;
using Howest.MagicCards.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Howest.MagicCards.DAL.Extensions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Howest.MagicCards.DAL.Repositories.Implementations
{
    public class CardRepository : ICardRepository
    {
        private readonly MagicCardsDbContext _context;

        public CardRepository(MagicCardsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(CardFilterRecord filter, int pageSize, int pageNumber, string sortBy, bool sortDescending)
        {
            IQueryable<Card> query = _context.Cards.IncludeStandardNavigation();

            query = ApplyFilter(query, filter);

            query = ApplySorting(query, sortBy, sortDescending);

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<Card> GetCardByIdAsync(long id)
        {
            return await _context.Cards.IncludeStandardNavigation()
                                       .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Card>> GetCardsByArtistIdAsync(long artistId)
        {
            return await _context.Cards.IncludeStandardNavigation()
                                       .Where(c => c.ArtistId == artistId)
                                       .ToListAsync();
        }

        public async Task<Card> GetCardDetailsAsync(int id)
        {
            return await _context.Cards.IncludeStandardNavigation()
                                       .FirstOrDefaultAsync(c => c.Id == id);
        }

        private IQueryable<Card> ApplyFilter(IQueryable<Card> query, CardFilterRecord filter)
        {
            if (!string.IsNullOrEmpty(filter.Set))
            {
                query = query.Where(c => c.SetCodeNavigation.Name.Contains(filter.Set));
            }

            if (!string.IsNullOrEmpty(filter.Artist))
            {
                query = query.Where(c => c.Artist.FullName.Contains(filter.Artist));
            }

            if (!string.IsNullOrEmpty(filter.Rarity))
            {
                query = query.Where(c => c.RarityCodeNavigation.Name.Contains(filter.Rarity));
            }

            if (!string.IsNullOrEmpty(filter.CardType))
            {
                query = query.Where(c => c.CardTypes.Any(ct => ct.Type.Name.Contains(filter.CardType)));
            }

            if (!string.IsNullOrEmpty(filter.CardName))
            {
                query = query.Where(c => c.Name.Contains(filter.CardName));
            }

            if (!string.IsNullOrEmpty(filter.CardText))
            {
                query = query.Where(c => c.Text.Contains(filter.CardText));
            }

            return query;
        }

        private IQueryable<Card> ApplySorting(IQueryable<Card> query, string sortBy, bool sortDescending)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = "Name";
            }

            IEntityType entityType = _context.Model.FindEntityType(typeof(Card));
            IProperty property = entityType.FindProperty(sortBy);
            if (property == null)
            {
                throw new ArgumentException($"Property '{sortBy}' does not exist on entity type '{entityType.Name}'");
            }

            if (sortDescending)
            {
                query = query.OrderByDescending(c => EF.Property<object>(c, sortBy));
            }
            else
            {
                query = query.OrderBy(c => EF.Property<object>(c, sortBy));
            }

            return query;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            IQueryable<Card> query = _context.Cards.IncludeStandardNavigation();

            return await query.ToListAsync();
        }
    }
}
