namespace Inventory.API.Exceptions
{
    public class ProductDeleteEventHandlerException : InternalServerException
    {
        public ProductDeleteEventHandlerException(string message) : base(message)
        {
        }
    }
}
