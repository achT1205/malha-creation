namespace Inventory.API.Exceptions;
public class StockNotFoundException : NotFoundException
{
    public StockNotFoundException(string? message) : base(message)
    {
    }
}