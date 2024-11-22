using Discount.Domain.Models;
using Discount.Domain.ValueObjects;

namespace Discount.Infrastructure.Extentions;

internal class InitialData
{
    public static IEnumerable<Coupon> Coupons => new List<Coupon>
     {
        Coupon.Create(
            CouponId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc11")),
            CouponCode.Of("SAVE10"),
            "10% Off Coupon",
            "Get 10% off your next purchase.",
            Discountable.Of(null, 10), // 10% discount
            DateTime.UtcNow.AddDays(-1), // Started yesterday
            DateTime.UtcNow.AddMonths(1), // Valid for one month
            100, // Max uses
            1, // Max uses per customer
            50m, // Minimum order value
            false, // Not restricted to first orders
            true // Active
            ),
        Coupon.Create(
            CouponId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc12")),
            CouponCode.Of("FLAT20"),
            "Flat $20 Off",
            "Flat $20 off on orders above $100.",
            Discountable.Of(20, null), // $20 flat discount
            DateTime.UtcNow, // Starts today
            DateTime.UtcNow.AddMonths(2), // Valid for two months
            50, // Max uses
            2, // Max uses per customer
            100m, // Minimum order value
            true, // Restricted to first orders
            true // Active
            ),
        Coupon.Create(
            CouponId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc13")),
            CouponCode.Of("FLAT20"),
            "Flat $20 Off",
            "Flat $20 off on all order.",
            Discountable.Of(20, null), // $20 flat discount
            null, // Starts today
            null, // Valid for two months
            null, // Max uses
            null, // Max uses per customer
            null, // Minimum order value
            false, // Restricted to first orders
            true // Active
            )
     };
}
