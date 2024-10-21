
namespace Catalog.Domain.Models;

public class AccessoryColorVariant : ColorVariant
{
    public Price Price { get; protected set; } = default!;
    public Quantity Quantity { get; protected set; } = default!;
    public AccessoryColorVariant(ProductId productId, Color color, Slug slug, Price price, Quantity quantity)
    : base(productId, color, slug)
    {
        Price = price;
        Quantity = quantity;
    }

    public static AccessoryColorVariant Create(ProductId productId, Color color, Slug slug, Price price, Quantity quantity)
    {
        return new AccessoryColorVariant(productId, color, slug, price, quantity);
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

    public override void AddImage(Image image)
    {
        if (_images.Contains(image))
            return;

        _images.Add(image);
    }
}
