namespace Howest.MagicCards.WebAPI;

public class PagingOptions
{
    public int MaxPageSize { get; set; }
    public PagingOptions Value { get; internal set; }
}

