

#nullable disable

namespace Howest.MagicCards.DAL.Entities
{
    public partial class CardColor
    {
        public long CardId { get; set; }
        public long ColorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Card Card { get; set; }
        public virtual Color Color { get; set; }
    }
}
