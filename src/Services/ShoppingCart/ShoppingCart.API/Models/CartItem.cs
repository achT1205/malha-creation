namespace Cart.API.Models;

public class CartItem
{
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Size { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
}