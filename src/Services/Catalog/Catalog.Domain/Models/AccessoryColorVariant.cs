namespace Catalog.Domain.Models;

public class AccessoryColorVariant : ColorVariantBase
{
    public Price Price { get; private set; }
    public Quantity Quantity { get; private set; }

    private AccessoryColorVariant(ColorVariantId id, Color color, Slug slug, List<Image> images, Price price, Quantity quantity)
        : base(id, color, slug, images)
    {
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
    }

    public static AccessoryColorVariant Create(Color color, Slug slug, List<Image> images, Price price, Quantity quantity)
    {
        return new AccessoryColorVariant(ColorVariantId.Of(Guid.NewGuid()), color, slug, images, price, quantity);
    }

    public void UpdatePrice(Price newPrice)
    {
        if (!Price.Equals(newPrice))
        {
            Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
        }
    }

    public void UpdateQuantity(Quantity newQuantity)
    {
        if (!Quantity.Equals(newQuantity))
        {
            Quantity = newQuantity ?? throw new ArgumentNullException(nameof(newQuantity));
        }
    }

    public override void AddSizeVariant(SizeVariant sizeVariant)
    {
        throw new NotSupportedException("AccessoryColorVariant does not support size variants.");
    }


    public override IReadOnlyCollection<SizeVariant> GetSizes()
    {
        return new List<SizeVariant>(); // Accessories don't have size variants, returning an empty list
    }
}
