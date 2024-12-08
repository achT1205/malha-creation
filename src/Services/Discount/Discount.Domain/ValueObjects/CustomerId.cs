using Discount.Domain.Exceptions;

namespace Discount.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; private set; }
    private CustomerId()
    {

    }
    private CustomerId(Guid value) => Value = value;
    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new CouponDomainException("CustomerId cannot be empty.");
        }

        return new CustomerId(value);
    }
}