using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.GraphQL.GraphQL.Types;

namespace Howest.MagicCards.GraphQL.GraphQL.Query
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(ICardRepository cardRepository, IArtistRepository artistRepository)
        {
            FieldAsync<ListGraphType<CardTypeG>>(
                "cards",
                resolve: async context => (await cardRepository.GetAllCardsAsync()).ToList()
            );

            FieldAsync<ListGraphType<ArtistType>>(
                "artists",
                resolve: async context => (await artistRepository.GetArtistsAsync()).ToList()
            );

            FieldAsync<CardTypeG>(
                "card",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    return await cardRepository.GetCardByIdAsync(id);
                }
            );

            FieldAsync<ArtistType>(
                "artist",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    return await artistRepository.GetArtistByIdAsync(id);
                }
            );
        }
    }
}
