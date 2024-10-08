using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.Orders.Models;

namespace Ordering.Infrastructure.Database.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id).HasConversion(
                                   orderItemId => orderItemId.Value,
                                   dbId => OrderItemId.Of(dbId));

        builder.Property(oi => oi.Quantity).IsRequired();

        builder.Property(oi => oi.Price).IsRequired();
        builder.Property(oi => oi.ProductName).IsRequired();
        builder.Property(oi => oi.Slug).IsRequired();
        builder.Property(oi => oi.Color);
        builder.Property(oi => oi.Size);
    }
}