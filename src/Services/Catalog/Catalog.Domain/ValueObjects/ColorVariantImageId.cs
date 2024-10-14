namespace Catalog.Domain.ValueObjects;

public record ColorVariantImageId
{
    public Guid Value { get; }
    private ColorVariantImageId(Guid value) => Value = value;
    public static ColorVariantImageId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ColorVariantImageId cannot be empty.");
        }

        return new ColorVariantImageId(value);
    }
}