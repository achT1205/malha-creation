namespace BuildingBlocks.Exceptions;

public class ProductNotFoundException : NotFoundException
{

    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {
    }
    public ProductNotFoundException(string? message) : base(message)
    {
    }

    public ProductNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}