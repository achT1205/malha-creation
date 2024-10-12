namespace Catalog.API.Exceptions;

public class NoRelatedStockFoundException : NotFoundException
{
    public NoRelatedStockFoundException(string? message) : base(message)
    {
    }
}
