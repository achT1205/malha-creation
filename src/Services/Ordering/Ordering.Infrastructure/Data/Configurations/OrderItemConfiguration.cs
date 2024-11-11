using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id).HasConversion(
                                   orderItemId => orderItemId.Value,
                                   dbId => OrderItemId.Of(dbId));

        builder.Property(e => e.OrderId)
              .HasConversion(
                  v => v.Value,
                  v => OrderId.Of(v));

        builder.Property(e => e.ProductId)
            .HasConversion(
              v => v.Value,
              v => ProductId.Of(v));

        builder.Property(oi => oi.Quantity).IsRequired();

        builder.Property(oi => oi.Price)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(oi => oi.ProductName).IsRequired();
        builder.Property(oi => oi.Slug).IsRequired();
        builder.Property(oi => oi.Color).IsRequired();
        builder.Property(oi => oi.Size);



    }
}