

namespace Howest.MagicCards.Shared.Dtos;

public class CardListDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string SetName { get; set; }
    public string ArtistName { get; set; }
    public string RarityName { get; set; }
    public List<string> CardTypeShortHands { get; set; }
    public string OriginalImageUrl { get; set; }
}
