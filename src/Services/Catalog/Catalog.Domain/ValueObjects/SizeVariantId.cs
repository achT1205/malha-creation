namespace Catalog.Domain.ValueObjects;

public record SizeVariantId
{
    public Guid Value { get; }
    private SizeVariantId(Guid value) => Value = value;
    public static SizeVariantId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("SizeVariantId cannot be empty.");
        }

        return new SizeVariantId(value);
    }
}