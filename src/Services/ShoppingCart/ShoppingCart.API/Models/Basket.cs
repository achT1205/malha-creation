namespace Cart.API.Models;
public class Basket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } = default!;
    public List<BasketItem> Items { get; set; } = new();
    public CouponModel? Coupon { get; set; } = null!;
    public decimal TotalPrice => 
        Coupon != null && !string.IsNullOrWhiteSpace(Coupon.CouponCode) ?
        Coupon.DiscountedPrice 
        : Items.Sum(x => x.Price * x.Quantity);

    public Basket(Guid userId)
    {
        UserId = userId;
    }

    public Basket()
    {
    }
}