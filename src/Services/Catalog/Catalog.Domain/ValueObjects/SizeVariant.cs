namespace Catalog.Domain.ValueObjects;


public class SizeVariant
{
    public Size Size { get; private set; } = default!;
    public Price Price { get; private set; }
    public Quantity Quantity { get; private set; }

    private SizeVariant( Size size, Price price, Quantity quantity)
    {
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
    }


    public static SizeVariant Create(Size size, Price price, Quantity quantity)
    {
        return new SizeVariant( size, price, quantity);
    }
}
