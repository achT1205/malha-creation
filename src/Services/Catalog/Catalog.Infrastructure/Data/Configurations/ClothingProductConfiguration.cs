
//using Catalog.Domain.Enums;

//public class ClothingProductConfiguration : IEntityTypeConfiguration<ClothingProduct>
//{
//    public void Configure(EntityTypeBuilder<ClothingProduct> builder)
//    {
//        builder.HasBaseType<Product>(); // Inherit common configurations from Product

//        // Configure specific properties for ClothingProduct
//        builder.Property(cp => cp.IsHandmade)
//            .IsRequired();

//        // Configure relationship to ColorVariants restricted to ClothingColorVariant type
//        builder.HasMany(cp => cp.ColorVariants)
//               .WithOne()
//               .HasForeignKey(cv => cv.ProductId)
//               .OnDelete(DeleteBehavior.Cascade)
//               .IsRequired(); // Ensure the ProductId is always set for ColorVariants

//        // Discriminator for ColorVariant subtype
//        builder.Metadata.FindNavigation(nameof(ClothingProduct.ColorVariants))
//            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

//        //builder.HasDiscriminator(p => p.ProductTypeEnum)
//        //    .HasValue<AccessoryProduct>(ProductTypeEnum.Accessory)
//        //    .HasValue<ClothingProduct>(ProductTypeEnum.Clothing);

//        // Map this entity type to the shared Products table in the TPH setup
//        builder.ToTable("Products");
//    }
//}



////namespace Catalog.Infrastructure.Data.Configurations;
////internal sealed class ClothingColorVariantConfiguration : IEntityTypeConfiguration<ClothingColorVariant>
////{
////    public void Configure(EntityTypeBuilder<ClothingColorVariant> builder)
////    {
////        ConfigureClothingColorVariant(builder);
////    }

////    private void ConfigureClothingColorVariant(EntityTypeBuilder<ClothingColorVariant> builder)
////    {
////        builder.OwnsMany(cv => cv.SizeVariants, svb =>
////        {
////            svb.ToTable("SizeVariants");

////            svb.WithOwner().HasForeignKey(nameof(ColorVariantId), nameof(ProductId));

////            svb.HasKey(nameof(SizeVariant.Id), nameof(ColorVariantId), nameof(ProductId));

////            svb.Property(sv => sv.Id)
////            .HasColumnName(nameof(SizeVariantId))
////            .ValueGeneratedNever()
////            .HasConversion(
////                id => id.Value,
////                dbId => SizeVariantId.Of(dbId));

////            svb.OwnsOne(cv => cv.Price, prb =>
////            {
////                prb.Property(p => p.Currency)
////                   .HasColumnName("Currency")
////                   .IsRequired();

////                prb.Property(p => p.Amount)
////                    .HasColumnName("Price")
////                    .IsRequired();
////            });


////            svb.OwnsOne(sv => sv.Size, qb =>
////            {
////                qb.Property(q => q.Value)
////                  .HasColumnName("Size")
////                  .HasMaxLength(5)
////                  .IsRequired();
////            });

////            svb.OwnsOne(sv => sv.Quantity, qb =>
////            {
////                qb.Property(q => q.Value)
////                  .HasColumnName("Quantity")
////                  .IsRequired();
////            });
////        });

////        builder.Navigation(cv => cv.SizeVariants).Metadata.SetField("_sizeVariants");
////        builder.Navigation(cv => cv.SizeVariants).UsePropertyAccessMode(PropertyAccessMode.Field);
////        builder.Metadata.FindNavigation(nameof(ClothingProduct.ColorVariants))!
////            .SetPropertyAccessMode(PropertyAccessMode.Field);
////    }
////}
