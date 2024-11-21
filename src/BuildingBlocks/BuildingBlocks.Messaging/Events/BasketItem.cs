namespace BuildingBlocks.Messaging.Events;

public class OrderBasketItem
{
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Size { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public Guid ColorVariantId { get; set; } = default!;
    public Guid? SizeVariantId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string CoverImage { get; set; } = default!;
    public CouponModel Coupon { get; set; } = null!;
}