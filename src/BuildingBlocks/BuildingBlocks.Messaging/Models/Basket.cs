namespace BuildingBlocks.Messaging.Models;

public class Basket
{
    public Guid Id { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    public List<BasketItem> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }

}
