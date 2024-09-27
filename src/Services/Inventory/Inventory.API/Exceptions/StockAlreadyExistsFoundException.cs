namespace Inventory.API.Exceptions;
public class StockAlreadyExistsFoundException : AlreadyExistsFoundException
{
    public StockAlreadyExistsFoundException(string? message) : base(message)
    {
    }
}