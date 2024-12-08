using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
                        orderId => orderId.Value,
                        dbId => OrderId.Of(dbId));

        builder.ComplexProperty(
            o => o.OrderCode, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderCode))
                    .IsRequired();
            });

        builder.ComplexProperty(
            o => o.CustomerId, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.CustomerId))
                    .IsRequired();
            });

        builder.HasMany(o => o.OrderItems)
        .WithOne()
        .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
       o => o.ShippingAddress, addressBuilder =>
       {
           addressBuilder.Property(a => a.FirstName)
               .HasMaxLength(50)
               .IsRequired();

           addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

           addressBuilder.Property(a => a.EmailAddress)
               .HasMaxLength(50);

           addressBuilder.Property(a => a.AddressLine)
               .HasMaxLength(180)
               .IsRequired();

           addressBuilder.Property(a => a.Country)
               .HasMaxLength(50);

           addressBuilder.Property(a => a.City)
               .HasMaxLength(50);

           addressBuilder.Property(a => a.ZipCode)
               .HasMaxLength(5)
               .IsRequired();
       });

        builder.ComplexProperty(
          o => o.BillingAddress, addressBuilder =>
          {
              addressBuilder.Property(a => a.FirstName)
                   .HasMaxLength(50)
                   .IsRequired();

              addressBuilder.Property(a => a.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

              addressBuilder.Property(a => a.EmailAddress)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.AddressLine)
                  .HasMaxLength(180)
                  .IsRequired();

              addressBuilder.Property(a => a.Country)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.City)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.ZipCode)
                  .HasMaxLength(5)
                  .IsRequired();
          });

        builder.ComplexProperty(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(p => p.CardHolderName)
                       .HasMaxLength(50);

                   paymentBuilder.Property(p => p.CardNumber)
                       .HasMaxLength(24)
                       .IsRequired();

                   paymentBuilder.Property(p => p.Expiration)
                       .HasMaxLength(10);

                   paymentBuilder.Property(p => p.CVV)
                       .HasMaxLength(3);

                   paymentBuilder.Property(p => p.PaymentMethod);
               });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft) // Default value
            .HasConversion(s => s.ToString(), // Converts enum to string before storing in the DB
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)) // Converts string back to enum when reading from DB
            .IsRequired(); // Ensure it's required


        builder.Property(o => o.TotalPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(oi => oi.CouponCode);
        builder.Property(oi => oi.DiscountDescription);
        builder.Property(oi => oi.OriginalPrice);
        builder.Property(oi => oi.DiscountedPrice);
        builder.Property(oi => oi.DiscountAmount);
        builder.Property(oi => oi.DiscountType);
        builder.Property(oi => oi.DiscountLabel);

    }
}

