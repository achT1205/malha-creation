namespace Catalog.Domain.ValueObjects;

public record ImageId
{
    public Guid Value { get; }
    private ImageId(Guid value) => Value = value;
    public static ImageId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ColorVariantImageId cannot be empty.");
        }

        return new ImageId(value);
    }
}