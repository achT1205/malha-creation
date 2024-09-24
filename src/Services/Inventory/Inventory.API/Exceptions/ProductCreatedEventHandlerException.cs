namespace Inventory.API.Exceptions;

public class ProductCreatedEventHandlerException : InternalServerException
{
    public ProductCreatedEventHandlerException(string message) : base(message)
    {
    }
}