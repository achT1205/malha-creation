using Discount.Domain.Exceptions;

namespace Discount.Domain.ValueObjects;

public record CouponId
{
    public Guid Value { get; private set; }
    private CouponId()
    {

    }
    private CouponId(Guid value) => Value = value;
    public static CouponId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new CouponDomainException("CouponId cannot be empty.");
        }

        return new CouponId(value);
    }
}