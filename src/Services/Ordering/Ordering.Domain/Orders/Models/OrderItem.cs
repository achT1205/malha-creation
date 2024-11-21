namespace Ordering.Domain.Orders.Models;

public class OrderItem : Entity<OrderItemId>
{
    internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price, string color, string? size, string productName, string slug, int? discount, string? descrition)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        Color = color;
        Size = size;
        ProductName = productName;
        Slug = slug;
        Discount = discount;
        CouponDescrition = descrition;
    }
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public string Color { get; set; } = default!;
    public string? Size { get; set; } = default!;
    public string ProductName { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string? CouponDescrition { get; private set; } = default!;
    public int? Discount { get; private set; } = default!;
}