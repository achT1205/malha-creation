namespace BuildingBlocks.Messaging.Events;

public class OrderBasket
{
    public Guid UserId { get; set; } = default!;
    public List<OrderBasketItem> Items { get; set; } = new();
    public CouponModel Coupon { get; set; } = null!;
    public decimal TotalPrice => 
        Coupon != null && !string.IsNullOrWhiteSpace(Coupon.CouponCode) ?
        Coupon.DiscountedPrice
        : Items.Sum(x => x.Price * x.Quantity);
}