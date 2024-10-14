namespace Catalog.Domain.ValueObjects;

public record ColorVariantId
{
    public Guid Value { get; }
    private ColorVariantId(Guid value) => Value = value;
    public static ColorVariantId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ColorVariantId cannot be empty.");
        }

        return new ColorVariantId(value);
    }
}