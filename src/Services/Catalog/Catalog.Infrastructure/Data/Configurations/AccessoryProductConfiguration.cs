
//using Catalog.Domain.Enums;

//namespace Catalog.Infrastructure.Data.Configurations;

//public class AccessoryProductConfiguration : IEntityTypeConfiguration<AccessoryProduct>
//{
//    public void Configure(EntityTypeBuilder<AccessoryProduct> builder)
//    {
//        builder.HasBaseType<Product>();  // Inherit configurations from Product

//        // Configure specific properties or constraints for AccessoryProduct
//        builder.Property(ap => ap.IsHandmade)
//            .IsRequired();

//        // Configure relationship to ColorVariants restricted to AccessoryColorVariant type
//        builder.HasMany(ap => ap.ColorVariants)
//               .WithOne()
//               .HasForeignKey(cv => cv.ProductId)
//               .OnDelete(DeleteBehavior.Cascade)
//               .IsRequired();  // Ensure the ProductId is always set for ColorVariants

//        // Discriminator for ColorVariant subtype
//        builder.Metadata.FindNavigation(nameof(AccessoryProduct.ColorVariants))
//            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

//        // Ensure AccessoryProduct can only contain AccessoryColorVariant items
//        builder.HasMany(ap => ap.ColorVariants)
//            .WithOne()
//            .HasForeignKey(cv => cv.ProductId);
//            //.Metadata.SetIsEagerLoaded(true);

//        // Map this entity type to the shared Products table in the TPH setup
//        builder.ToTable("Products");
//    }
//}


////public class AccessoryProductConfiguration : IEntityTypeConfiguration<AccessoryProduct>
////{
////    public void Configure(EntityTypeBuilder<AccessoryProduct> builder)
////    {
////        builder.HasBaseType<Product>();

////        // Configure relationships specific to AccessoryProduct if needed
////        builder.HasMany(ap => ap.ColorVariants)
////               .WithOne()
////               .HasForeignKey(cv => cv.ProductId)
////               .OnDelete(DeleteBehavior.Cascade);  // Define delete behavior for related color variants

////        // Define indexes if necessary
////        builder.HasIndex(ap => ap.UrlFriendlyName)
////               .IsUnique(); // Ensure URL-friendly names are unique for SEO purposes

////        // Optional: Add a custom query filter if needed
////        builder.HasQueryFilter(ap => ap.ProductTypeEnum == ProductTypeEnum.Accessory);

////        // Map this entity type to the shared Products table in the TPH setup
////        builder.ToTable("Products");

////        //builder.OwnsMany(ap => ap.ColorVariants, cvb =>
////        //{

////        //    cvb.ToTable(nameof(Product.ColorVariants));

////        //    cvb.WithOwner().HasForeignKey(nameof(ColorVariant.ProductId));

////        //    cvb.HasKey(cv => new { cv.Id, cv.ProductId });

////        //    cvb.Property(cv => cv.Id)
////        //    .HasColumnName(nameof(ColorVariantId))
////        //    .ValueGeneratedNever()
////        //    .HasConversion(
////        //        id => id.Value,
////        //        dbId => ColorVariantId.Of(dbId));

////        //    cvb.OwnsOne(cv => cv.Color, cb =>
////        //    {
////        //        cb.Property(c => c.Value)
////        //          .HasColumnName("Color")
////        //          .IsRequired()
////        //          .HasMaxLength(50);
////        //    });

////        //    cvb.OwnsOne(cv => cv.Slug, slb =>
////        //    {
////        //        slb.Property(c => c.Value)
////        //          .HasColumnName("Slug")
////        //          .IsRequired()
////        //          .HasMaxLength(200);
////        //    });


////        //    //cvb.OwnsOne(cv => cv.Price, prb =>
////        //    //{
////        //    //    prb.Property(p => p.Currency)
////        //    //     .HasColumnName("Currency")
////        //    //     .IsRequired();

////        //    //    prb.Property(p => p.Amount)
////        //    //    .HasColumnName("Price")
////        //    //    .IsRequired();
////        //    //});

////        //});






////            //builder.OwnsOne(cv => cv.Quantity, qb =>
////            //{
////            //    qb.Property(q => q.Value)
////            //      .HasColumnName("Quantity")
////            //      .IsRequired();
////            //});

////            //builder.Metadata.FindNavigation(nameof(AccessoryProduct.ColorVariants))!
////            //    .SetPropertyAccessMode(PropertyAccessMode.Field);
////        }
////}