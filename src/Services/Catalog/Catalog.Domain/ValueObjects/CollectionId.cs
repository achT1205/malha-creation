﻿namespace Catalog.Domain.ValueObjects;

public record CollectionId
{
    public Guid Value { get; }
    private CollectionId(Guid value) => Value = value;
    public static CollectionId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("CollectionId cannot be empty.");
        }

        return new CollectionId(value);
    }
}