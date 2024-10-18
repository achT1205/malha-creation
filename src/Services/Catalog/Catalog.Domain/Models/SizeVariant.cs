namespace Catalog.Domain.Models;

public class SizeVariant : Entity<SizeVariantId>
{
    public ColorVariantId ColorVariantId { get; private set; } = default!;      
    public SizeVariantId SizeVariantId { get; private set; } = default!;
    public Size Size { get; private set; } = default!;
    public Price Price { get; private set; } = default!;
    public Quantity Quantity { get; private set; } = default!;

    private SizeVariant()
    {
        
    }
    private SizeVariant(ColorVariantId colorVariantId, SizeVariantId sizeVariantId, Size size, Price price, Quantity quantity)
    {
        ColorVariantId = colorVariantId ?? throw new ArgumentNullException(nameof(colorVariantId)); ;
        SizeVariantId = sizeVariantId ?? throw new ArgumentNullException(nameof(sizeVariantId)); ;
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
    }

    public static SizeVariant Create(ColorVariantId colorVariantId, SizeVariantId sizeVariantId, Size size, Price price, Quantity quantity)
    {
        return new SizeVariant(colorVariantId, sizeVariantId, size, price, quantity);
    }
}
