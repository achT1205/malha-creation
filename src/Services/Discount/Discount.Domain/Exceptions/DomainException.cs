namespace Discount.Domain.Exceptions;
public class CouponDomainException : Exception
{
    public CouponDomainException(string message)
        : base($"Domain Exception: \"{message}\" throws from Domain Layer.")
    {
    }
}