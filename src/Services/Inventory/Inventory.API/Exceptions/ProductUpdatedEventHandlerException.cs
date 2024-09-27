namespace Inventory.API.Exceptions;
public class ProductUpdatedEventHandlerException : InternalServerException
{
    public ProductUpdatedEventHandlerException(string message) : base(message)
    {
    }
}