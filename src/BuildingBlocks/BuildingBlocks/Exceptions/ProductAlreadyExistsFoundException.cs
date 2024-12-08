namespace BuildingBlocks.Exceptions;

public class ProductAlreadyExistsFoundException : Exception
{
    public ProductAlreadyExistsFoundException(string? message) : base(message)
    {
    }

    public ProductAlreadyExistsFoundException(string name, object key) : base($"Entity \"{name}\" ({key})already exists.")
    {
    }
}