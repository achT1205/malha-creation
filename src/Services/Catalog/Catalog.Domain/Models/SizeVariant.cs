using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;

public class SizeVariant : Entity<SizeVariantId>
{
    public ColorVariantId ColorVariantId { get; private set; } = default!;
    public Size Size { get; private set; } = default!;
    public Price Price { get; private set; } = default!;
    public bool OnOrdering { get; private set; } = default!;
    public Quantity Quantity { get; private set; } = default!;

    // Available stock at which we should reorder
    public Quantity RestockThreshold { get; private set; } = default!;

    private SizeVariant()
    {

    }
    private SizeVariant(ColorVariantId colorVariantId, Size size, Price price, Quantity quantity, Quantity restockThreshold)
    {
        ColorVariantId = colorVariantId ?? throw new ArgumentNullException(nameof(colorVariantId)); ;
        Id = SizeVariantId.Of(Guid.NewGuid());
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
        RestockThreshold = restockThreshold ?? throw new ArgumentNullException(nameof(restockThreshold));
    }

    public static SizeVariant Create(ColorVariantId colorVariantId, Size size, Price price, Quantity quantity, Quantity restockThreshold)
    {
        return new SizeVariant(colorVariantId, size, price, quantity, restockThreshold);
    }

    public void Update(Size newSize, Price newPrice, Quantity newQuantity, Quantity newRestockThreshold)
    {
        if (!Size.Equals(newSize))
        {
            Size = newSize ?? throw new ArgumentNullException(nameof(newSize));
        }
        if (!Price.Equals(newPrice))
        {
            Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
        }
        if (!Quantity.Equals(newQuantity))
        {
            Quantity = newQuantity ?? throw new ArgumentNullException(nameof(newQuantity));
        }
        if (!RestockThreshold.Equals(newRestockThreshold))
        {
            RestockThreshold = newRestockThreshold ?? throw new ArgumentNullException(nameof(newRestockThreshold));
        }
    }
    public void AddStock(int newQuantity)
    {

        if (newQuantity <= 0)
        {
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        }
        Quantity = Quantity.Increase(newQuantity);
    }
    public void RemoveStock(int newQuantity, string slug)
    {
        if (Quantity.Equals(0))
        {
            throw new CatalogDomainException($"Empty stock, product item {slug} is sold out");
        }

        if (newQuantity <= 0)
        {
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        }
        Quantity = Quantity.Decrease(newQuantity);
    }
}
