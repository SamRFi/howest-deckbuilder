
using Howest.MagicCards.DAL.Contexts;
using Howest.MagicCards.DAL.Entities;
using Howest.MagicCards.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories.Implementations;

public class SetRepository : ISetRepository
{
    private readonly MagicCardsDbContext _context;

    public SetRepository(MagicCardsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Set>> GetSetsAsync()
    {
        return await _context.Sets.ToListAsync();
    }
}
