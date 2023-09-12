using System;

using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.DAL.Repositories.Interfaces;

public interface IDeckRepository
{   
    Task AddCardToDeckAsync(Card card);
    Task DeleteCardFromDeckAsync(long cardId);
    Task<List<Card>> GetDeckAsync();
    Task ClearDeckAsync();
}

