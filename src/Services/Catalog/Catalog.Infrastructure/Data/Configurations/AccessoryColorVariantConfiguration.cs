//namespace Catalog.Infrastructure.Data.Configurations;
//public class AccessoryColorVariantConfiguration : IEntityTypeConfiguration<AccessoryColorVariant>
//{
//    public void Configure(EntityTypeBuilder<AccessoryColorVariant> builder)
//    {
//        builder.HasBaseType<ColorVariant>(); // Inherit from ColorVariant

//        // Map to its own table
//        builder.Property(acv => acv.Price).IsRequired();
//        builder.Property(acv => acv.Quantity).IsRequired();
//        builder.ToTable("AccessoryColorVariants");
//    }
//}
