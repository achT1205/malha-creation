namespace Ordering.Application.Orders.Models;

public class BasketItem
{
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Size { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string Slug { get; set; } = default!; 
}