using Discount.Domain.Models;
using Discount.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Infrastructure.Data.Configurations;
internal sealed class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        ConfigureCouponsTable(builder);
        ConfigureProductsTable(builder);
        ConfigureCustomersTable(builder);
    }

    private void ConfigureProductsTable(EntityTypeBuilder<Coupon> builder)
    {

        builder.OwnsMany(c => c.ProductIds, cb =>
        {
            cb.ToTable("CouponProduct");

            cb.WithOwner().HasForeignKey(nameof(CouponId));

            cb.HasKey("Id");

            cb.Property(r => r.Value)
            .HasColumnName("ProductId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Coupon.ProductIds))!
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureCustomersTable(EntityTypeBuilder<Coupon> builder)
    {

        builder.OwnsMany(c => c.AllowedCustomerIds, cb =>
        {
            cb.ToTable("CouponCustomer");

            cb.WithOwner().HasForeignKey(nameof(CouponId));

            cb.HasKey("Id");

            cb.Property(r => r.Value)
            .HasColumnName("CustomerId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Coupon.AllowedCustomerIds))!
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureCouponsTable(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupon");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                dbId => CouponId.Of(dbId));

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.ComplexProperty(
            c => c.Code, cb =>
            {
                cb.Property(c => c.Value)
                    .HasColumnName(nameof(Coupon.Code))
                    .HasMaxLength(15)
                    .IsRequired();
            });

        builder.Property(c => c.Description).HasMaxLength(500);

        builder.ComplexProperty(
            c => c.Discountable, db =>
            {
                db.Property(d => d.FlatAmount)
                    .HasColumnName(nameof(Discountable.FlatAmount))
                    .IsRequired(false);
                db.Property(d => d.Percentage)
                    .HasColumnName(nameof(Discountable.Percentage))
                    .IsRequired(false);
            });
        builder.Property(c => c.StartDate).HasColumnType("DateTime").HasColumnName("StartDate");
        builder.Property(c => c.EndDate).HasColumnType("DateTime").HasColumnName("EndDate");
        builder.Property(c => c.MaxUses);
        builder.Property(c => c.TotalRedemptions);
        builder.Property(c => c.MaxUsesPerCustomer);
        builder.Property(c => c.MinimumOrderValue);
        builder.Property(c => c.IsFirstTimeOrderOnly);
        builder.Property(c => c.IsActive);

    }
}