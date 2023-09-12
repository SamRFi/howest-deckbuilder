using GraphQL.Types;
using Howest.MagicCards.DAL.Entities;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class ArtistType : ObjectGraphType<Artist>
    {
        public ArtistType()
        {
            Name = "Artist";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the artist.");
            Field(x => x.FullName).Description("The full name of the artist.");
            Field<ListGraphType<CardTypeG>>("cards",
                resolve: context => context.Source.Cards);
        }
    }
}
