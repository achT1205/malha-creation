using BuildingBlocks.Exceptions;

namespace Cart.API.Exceptions;
public class CartNotFoundException : NotFoundException
{
    public CartNotFoundException(string? message) : base(message)
    {
    }
}
