namespace Discount.Grpc.Models;

public class CartCoupon
{
    public int Id { get; set; }
    public string CouponCode { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int discountRate { get; set; }
}