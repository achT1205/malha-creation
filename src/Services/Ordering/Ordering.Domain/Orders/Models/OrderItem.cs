namespace Ordering.Domain.Orders.Models;

public class OrderItem : Entity<OrderItemId>
{
    internal OrderItem(
        OrderId orderId, 
        ProductId productId, 
        int quantity, 
        decimal price, 
        string color, 
        string? size, 
        string productName, 
        string slug,
        string? couponCode ,
        string? description ,
        decimal? originalPrice ,
        decimal? discountedPrice ,
        decimal? discountAmount ,
        string? discountType ,
        string? discountLabel 
        )
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
        CouponCode = couponCode;
        DiscountDescription = description;
        DiscountType = discountType;
        DiscountLabel = discountLabel;
        OriginalPrice = originalPrice;
        DiscountAmount = discountAmount;
        DiscountedPrice = discountedPrice;

    }

    public OrderItem()
    {
        
    }
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string? Size { get; private set; } = default!;
    public string ProductName { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string ?  CouponCode { get; private set; } = string.Empty; // The coupon code
    public string? DiscountDescription { get; private set;} = string.Empty; // Coupon description
    public decimal? OriginalPrice { get; private set;} // The original product price
    public decimal? DiscountedPrice { get; private set;} // The discounted product price
    public decimal? DiscountAmount { get; private set;} // The calculated discount amount
    public string? DiscountType { get; private set;} = string.Empty; // FlatAmount or Percentage
    public string? DiscountLabel { get; private set;} = string.Empty;
}