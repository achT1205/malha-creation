﻿



namespace Catalog.Infrastructure.Data.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureProductsTable(builder);
        ConfigureColorVariantsTable(builder);
        ConfigureCategoriesTable(builder);
        ConfigureOccasionsTable(builder);
        ConfigureProductReviewIdsTable(builder);
    }

    private void ConfigureProductReviewIdsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.ProductReviewIds, rib =>
        {
            rib.ToTable("ProductReviewIds");

            rib.WithOwner().HasForeignKey("ProductId");

            rib.HasKey("Id");

            rib.Property(r => r.Value)
            .HasColumnName("ReviewId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Product.ProductReviewIds))!
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureOccasionsTable(EntityTypeBuilder<Product> builder)
    {

        builder.OwnsMany(p => p.OccasionIds, ob =>
        {
            ob.ToTable("ProductOccasionIds");

            ob.WithOwner().HasForeignKey("ProductId");

            ob.HasKey("Id");

            ob.Property(r => r.Value)
            .HasColumnName("OccasionId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Product.OccasionIds))!
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureCategoriesTable(EntityTypeBuilder<Product> builder)
    {

        builder.OwnsMany(p => p.CategoryIds, cb =>
        {
            cb.ToTable("ProductCategoryIds");

            cb.WithOwner().HasForeignKey("ProductId");

            cb.HasKey("Id");

            cb.Property(r => r.Value)
            .HasColumnName("CategoryId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Product.CategoryIds))!
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureColorVariantsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.ColorVariants, cvb =>
        {
            cvb.ToTable(nameof(Product.ColorVariants));

            cvb.WithOwner().HasForeignKey(nameof(ColorVariant.ProductId));

            cvb.HasKey(cv => new { cv.Id, cv.ProductId });

            cvb.Property(cv => cv.Id)
            .HasColumnName(nameof(ColorVariantId))
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                dbId => ColorVariantId.Of(dbId));

            cvb.OwnsOne(cv => cv.Color, cb =>
            {
                cb.Property(c => c.Value)
                  .HasColumnName("Color")
                  .IsRequired()
                  .HasMaxLength(50);
            });

            cvb.OwnsOne(cv => cv.Slug, slb =>
            {
                slb.Property(c => c.Value)
                  .HasColumnName("Slug")
                  .IsRequired()
                  .HasMaxLength(200);
            });

            cvb.OwnsOne(cv => cv.Price, prb =>
            {
                prb.OwnsOne(p => p.Currency, cb =>
                {
                    cb.Property(c => c.Value)
                      .HasColumnName("Currency")
                      .IsRequired()
                      .HasMaxLength(Currency.Length);
                });

                prb.Property(p => p.Amount)
                .HasColumnName("Amount")
                .IsRequired();
            });

            cvb.OwnsOne(cv => cv.Quantity, qb =>
            {
                qb.Property(q => q.Value)
                  .HasColumnName("Quantity")
                  .IsRequired()
                  .HasMaxLength(200);
            });

            cvb.OwnsMany(cv => cv.Images, imsb =>
            {
                imsb.ToTable("ColorVariantImages");
            });

            //cvb.OwnsMany(cv => cv.SizeVariants, svb =>
            //{
            //    svb.ToTable("SizeVariants");

            //    svb.WithOwner().HasForeignKey(nameof(ColorVariantId), nameof(ProductId));

            //    svb.HasKey(nameof(SizeVariant.Id), nameof(ColorVariantId), nameof(ProductId));

            //    svb.Property(sv => sv.Id)
            //    .HasColumnName(nameof(SizeVariantId))
            //    .ValueGeneratedNever()
            //    .HasConversion(
            //        id => id.Value,
            //        dbId => SizeVariantId.Of(dbId));

            //    svb.OwnsOne(cv => cv.Size);

            //    svb.OwnsOne(cv => cv.Price, prb =>
            //    {
            //        prb.OwnsOne(p => p.Currency, cb =>
            //        {
            //            cb.Property(c => c.Value)
            //              .HasColumnName("Currency")
            //              .IsRequired()
            //              .HasMaxLength(Currency.Length);
            //        });
            //    });

            //    svb.OwnsOne(cv => cv.Quantity);


            //});

            //cvb.Navigation(cv => cv.SizeVariants).Metadata.SetField("_sizeVariants");
            //cvb.Navigation(cv => cv.SizeVariants).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(Product.ColorVariants))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                dbId => ProductId.Of(dbId));

        builder.ComplexProperty(
        p => p.Name, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName(nameof(Product.Name))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(
                p => p.UrlFriendlyName, unb =>
                {
                    unb.Property(n => n.Value)
                        .HasColumnName(nameof(Product.UrlFriendlyName))
                        .HasMaxLength(100)
                        .IsRequired();
                });

        builder.ComplexProperty(
                p => p.Description, desb =>
                {
                    desb.Property(n => n.Value)
                        .HasColumnName(nameof(Product.Description))
                        .HasMaxLength(100)
                        .IsRequired();
                });

        builder.ComplexProperty(
          p => p.AverageRating, avb =>
          {
              avb.Property(av => av.Value)
                  .HasColumnName(nameof(Product.AverageRating))
                  .IsRequired();

              avb.Property(av => av.TotalRatingsCount)
                 .HasColumnName(nameof(Product.AverageRating.TotalRatingsCount))
                 .IsRequired();
          });

        builder.ComplexProperty(
              p => p.CoverImage, cib =>
              {
                  cib.Property(ci => ci.ImageSrc)
                      .HasColumnName(nameof(Product.CoverImage.ImageSrc))
                      .IsRequired();

                  cib.Property(ci => ci.AltText)
                     .HasColumnName(nameof(Product.CoverImage.AltText))
                     .IsRequired();
              });


        //builder.OwnsOne(p => p.Name);
        //builder.OwnsOne(p => p.UrlFriendlyName);
        //builder.OwnsOne(p => p.Description);
        //builder.OwnsOne(p => p.AverageRating);

        //builder.OwnsOne(p => p.CoverImage);

        builder.Property(p => p.IsHandmade);

        builder.Property(p => p.ProductTypeId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => ProductTypeId.Of(dbId));

        builder.Property(p => p.MaterialId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => MaterialId.Of(dbId));

        builder.Property(p => p.CollectionId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => CollectionId.Of(dbId));
    }
}
