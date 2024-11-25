namespace BuildingBlocks.Exceptions;

public class ProductOutOfStockFoundException : Exception
{
    public ProductOutOfStockFoundException(string? message) : base(message)
    {
    }

    public ProductOutOfStockFoundException(string name, object key) : base($"Entity \"{name}\" ({key})already exists.")
    {
    }
}