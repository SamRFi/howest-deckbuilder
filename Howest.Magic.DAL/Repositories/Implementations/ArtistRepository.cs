
using Howest.MagicCards.DAL.Contexts;
using Howest.MagicCards.DAL.Entities;
using Howest.MagicCards.DAL.Repositories.Interfaces;
using Howest.MagicCards.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories.Implementations;

public class ArtistRepository : IArtistRepository
{
    private readonly MagicCardsDbContext _context;

    public ArtistRepository(MagicCardsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Artist>> GetArtistsAsync()
    {
        return await _context.Artists.Include(a => a.Cards).ToListAsync();
    }

    public async Task<Artist> GetArtistByIdAsync(long id)
    {
        Artist artist = await _context.Artists.Include(a => a.Cards).WithId(id).FirstOrDefaultAsync();
        return artist is not null ? artist : throw new KeyNotFoundException($"Artist with id {id} not found");
    }

}
