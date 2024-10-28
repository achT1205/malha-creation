//namespace Catalog.Infrastructure.Data.Configurations;

//public class ClothingColorVariantConfiguration : IEntityTypeConfiguration<ClothingColorVariant>
//{
//    public void Configure(EntityTypeBuilder<ClothingColorVariant> builder)
//    {
//        builder.HasBaseType<ColorVariant>(); // Inherit common configurations from ColorVariant

//        // Configure relationship to SizeVariants
//        builder.HasMany(ccv => ccv.SizeVariants)
//               .WithOne()
//               .HasForeignKey(sv => sv.ColorVariantId)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior for SizeVariants

//        // Configure properties for SizeVariants within ClothingColorVariant
//        builder.Navigation(ccv => ccv.SizeVariants)
//               .UsePropertyAccessMode(PropertyAccessMode.Field); // Use field-based access

//        builder.ToTable("ClothingColorVariants");
//    }
//}
