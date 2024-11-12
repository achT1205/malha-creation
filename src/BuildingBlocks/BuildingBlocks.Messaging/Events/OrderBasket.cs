namespace BuildingBlocks.Messaging.Events;

public class OrderBasket
{
    public Guid UserId { get; set; } = default!;
    public List<OrderBasketItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}