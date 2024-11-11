namespace Cart.API.Models;
public class Basket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } = default!;
    public bool Checkout { get; set; } = false;
    public List<BasketItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public Basket(Guid userId)
    {
        UserId = userId;
    }

    public Basket()
    {
    }
}