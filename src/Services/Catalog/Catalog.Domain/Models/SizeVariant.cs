namespace Catalog.Domain.Models;

public class SizeVariant : Entity<SizeVariantId>
{
    public ColorVariantId ColorVariantId  { get; private set; }
    public Size Size { get; private set; } = default!;
    public Price Price { get; private set; }

    public SizeVariant(ColorVariantId colorVariantId, Size size, Price price, int quantity)
    {
        Id = SizeVariantId.Of(Guid.NewGuid());
        ColorVariantId = colorVariantId;
        Size = size;
        Price = price;
    }
}
