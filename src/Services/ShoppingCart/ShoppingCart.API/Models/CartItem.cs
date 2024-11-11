namespace Cart.API.Models;

public class CartItem
{
    public int Quantity { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public Guid ColorVariantId { get; set; } = default!;
    public Guid? SizeVariantId { get; set; } = default!;
}