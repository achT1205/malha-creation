using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;


public class DiscountContext : DbContext
{

    public DbSet<Coupon> Coupons { get; set; } = default!;
    public DbSet<CartCoupon> CartCoupons { get; set; } = default!;


    public DiscountContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductId = "019235b3-4946-40ae-8078-710e338b7b4a", Description = "Get $5 off on product purchase", Amount = 5 },
            new Coupon { Id = 2, ProductId = "019235b3-4946-40ae-8078-710e338b7b4a", Description = "ISpecial discount of $10 on selected item", Amount = 10 });
        modelBuilder.Entity<CartCoupon>().HasData(
            new CartCoupon { Id = 1, CouponCode = "SAVE10", Description = "Get 10% off on your purchase", discountRate = 10 },
            new CartCoupon { Id = 2, CouponCode = "FREESHIP", Description = "Free shipping on orders over $50", discountRate = 0 });
    }
}