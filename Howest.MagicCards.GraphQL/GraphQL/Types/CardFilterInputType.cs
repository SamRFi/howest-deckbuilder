using GraphQL.Types;
using Howest.MagicCards.DAL;

namespace Howest.MagicCards.GraphQL.GraphQL.Types;

public class CardFilterInputType : InputObjectGraphType<CardFilterRecord>
{
    public CardFilterInputType()
    {
        Field(f => f.Set, nullable: true);
        Field(f => f.Artist, nullable: true);
        Field(f => f.Rarity, nullable: true);
        Field(f => f.CardType, nullable: true);
        Field(f => f.CardName, nullable: true);
        Field(f => f.CardText, nullable: true);
    }
}
