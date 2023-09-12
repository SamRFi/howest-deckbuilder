using GraphQL.Types;
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardTypeG : ObjectGraphType<Card>
    {
        public CardTypeG()
        {
            Name = "Card";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the card.");
            Field(x => x.Name).Description("The name of the card.");
            Field(x => x.Type).Description("The type of the card.");
            Field(x => x.RarityCode).Description("The rarity code of the card.");
            Field(x => x.SetCode).Description("The set code of the card.");
            Field(x => x.Text).Description("The text on the card.");
            Field(x => x.MultiverseId, nullable: true).Description("The multiverse ID of the card.");
            Field(x => x.OriginalImageUrl).Description("The URL of the original image for the card.");

            Field<ArtistType>("artist", resolve: context => context.Source.Artist);
        }
    }
}
