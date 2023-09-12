
using Howest.MagicCards.DAL.Contexts;
using Howest.MagicCards.DAL.Entities;
using Howest.MagicCards.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories.Implementations;

public class RarityRepository : IRarityRepository
{
    private readonly MagicCardsDbContext _context;

    public RarityRepository(MagicCardsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rarity>> GetRaritiesAsync()
    {
        return await _context.Rarities.ToListAsync();
    }
}
