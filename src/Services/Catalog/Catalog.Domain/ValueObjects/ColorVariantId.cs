namespace Catalog.Domain.ValueObjects;

public record ColorVariantId
{
    public Guid Value { get; private set; }

   
    private ColorVariantId(Guid value)
    {
        Value = value != Guid.Empty ? value : throw new ArgumentException("ColorVariantId cannot be empty.", nameof(value));
    }

    private ColorVariantId()  { }

    public static ColorVariantId Of(Guid value)
    {
        return new ColorVariantId(value);
    }
}
