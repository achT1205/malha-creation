namespace Discount.Application.Exceptions;


public class CouponAlreadyExistsFoundException : Exception
{
    public CouponAlreadyExistsFoundException(string? message) : base(message)
    {
    }

    public CouponAlreadyExistsFoundException(string name, object key) : base($"Entity \"{name}\" ({key})already exists.")
    {
    }
}