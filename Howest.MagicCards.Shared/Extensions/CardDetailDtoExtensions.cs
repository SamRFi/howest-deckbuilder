
using Howest.MagicCards.Shared.Dtos;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardDetailDtoExtensions
{
    public static bool IsFromSet(this CardDetailDto card, string setName)
    {
        return card.SetName.Equals(setName, StringComparison.OrdinalIgnoreCase);
    }
}

