﻿using Discount.Domain.Exceptions;

namespace Discount.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; private set; }
    private ProductId()
    {

    }
    private ProductId(Guid value) => Value = value;
    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new CouponDomainException("ProductId cannot be empty.");
        }

        return new ProductId(value);
    }
}