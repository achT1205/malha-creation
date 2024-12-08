namespace BuildingBlocks.Messaging.Events
{
    public class CouponModel
    {
        public string CouponCode { get; init; } = string.Empty; // The coupon code
        public string Description { get; init; } = string.Empty; // Coupon description
        public decimal OriginalPrice { get; init; } // The original product price
        public decimal DiscountedPrice { get; init; } // The discounted product price
        public decimal DiscountAmount { get; init; } // The calculated discount amount
        public string DiscountType { get; init; } = string.Empty; // FlatAmount or Percentage
        public string DiscountLabel { get; init; } = string.Empty;
    }
}