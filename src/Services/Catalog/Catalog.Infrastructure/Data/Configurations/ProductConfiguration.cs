using Catalog.Domain.Enums;

namespace Catalog.Infrastructure.Data.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureProductsTable(builder);
        ConfigureColorVariantsTable(builder);
        ConfigureCategoryIdsTable(builder);
        ConfigureOutfitIdsTable(builder);
        ConfigureOccasionIdsTable(builder);
        ConfigureProductReviewsTable(builder);
    }

    private void ConfigureOutfitIdsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.ColorVariants, cvb =>
        {
            cvb.OwnsMany(cvb => cvb.Outfits, oufb =>
            {
                oufb.ToTable("ColorVariantOutfit");
                oufb.Property(r => r.Value)
                   .HasColumnName("OutfitId")
                   .ValueGeneratedNever();

            });

        });
        //builder.Metadata.FindNavigation(nameof(ColorVariant.Outfits))!
        //    .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureProductReviewsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.Reviews, rib =>
        {
            rib.ToTable(nameof(Product.Reviews));

            rib.WithOwner().HasForeignKey(nameof(Review.ProductId));

            rib.HasKey(r => r.Id);

            rib.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => ReviewId.Of(dbId));

            rib.Property(r => r.ReviewerId)
            .HasColumnName("ReviewerId")
            .ValueGeneratedNever()
            .IsRequired();

            rib.Property(r => r.ReviewerId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => UserId.Of(dbId));

            rib.Property(r => r.DatePosted)
            .HasColumnName("DatePosted")
            .ValueGeneratedNever()
            .IsRequired();

            rib.Property(r => r.Comment)
            .HasColumnName("Comment")
            .ValueGeneratedNever()
            .IsRequired();

            rib.OwnsOne(r => r.Rating, rb =>
            {
                rb.Property(r => r.Value)
                  .HasColumnName("Rating")
                  .IsRequired();
            });
        });

        builder.Metadata.FindNavigation(nameof(Product.Reviews))!
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureOccasionIdsTable(EntityTypeBuilder<Product> builder)
    {

        builder.OwnsMany(p => p.OccasionIds, ob =>
        {
            ob.ToTable("ProductOccasion");

            ob.WithOwner().HasForeignKey("ProductId");

            ob.HasKey("Id");

            //ob.HasOne<Occasion>()
            // .WithMany()
            // .HasForeignKey("OccasionId")
            // .IsRequired()
            // .OnDelete(DeleteBehavior.Restrict);

            ob.Property(r => r.Value)
            .HasColumnName("OccasionId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Product.OccasionIds))!
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureCategoryIdsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(p => p.CategoryIds, cb =>
        {
            cb.ToTable("ProductCategory");

            cb.WithOwner().HasForeignKey("ProductId");

            cb.HasKey("Id");

            //cb.HasOne<Category>()
            //  .WithMany()
            //  .HasForeignKey("CategoryId")
            //  .IsRequired()
            //  .OnDelete(DeleteBehavior.Restrict);

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

            cvb.Property(cv => cv.OnOrdering);

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

            // Define the shadow property to enforce a unique constraint on UrlFriendlyName.Value
            cvb.Property<string>("Slug_Value")
                  .HasColumnName("Slug")
                  .HasMaxLength(200)
                  .IsRequired();

            // Set the unique constraint on the shadow property
            cvb.HasIndex("Slug_Value").IsUnique();

            cvb.OwnsOne(cv => cv.Slug, slb =>
            {
                slb.Property(c => c.Value)
                  .HasColumnName("Slug")
                  .IsRequired()
                  .HasMaxLength(200);
            });


            cvb.OwnsOne(cv => cv.Price, prb =>
            {
                prb.Property(p => p.Currency)
                 .HasColumnName("Currency");

                prb.Property(p => p.Amount)
                .HasColumnName("Price");
            });

            cvb.OwnsOne(cv => cv.Quantity, qb =>
            {
                qb.Property(q => q.Value)
                  .HasColumnName("Quantity");
            });

            cvb.OwnsOne(sv => sv.RestockThreshold, rb =>
            {
                rb.Property(r => r.Value)
                  .HasColumnName("RestockThreshold");
            });

            cvb.OwnsMany(cv => cv.Images, imsb =>
            {
                imsb.ToTable("ColorVariantImages");
            });

 

            cvb.OwnsMany(cv => cv.SizeVariants, svb =>
            {
                svb.ToTable("SizeVariants");

                svb.WithOwner().HasForeignKey(nameof(ColorVariantId), nameof(ProductId));

                svb.HasKey(nameof(SizeVariant.Id), nameof(ColorVariantId), nameof(ProductId));

                svb.Property(sv => sv.OnOrdering);

                svb.Property(sv => sv.Id)
                .HasColumnName(nameof(SizeVariantId))
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => SizeVariantId.Of(dbId));

                svb.OwnsOne(cv => cv.Price, prb =>
                {
                    prb.Property(p => p.Currency)
                       .HasColumnName("Currency")
                       .IsRequired();

                    prb.Property(p => p.Amount)
                        .HasColumnName("Price")
                        .IsRequired();
                });


                svb.OwnsOne(sv => sv.Size, sb =>
                {
                    sb.Property(s => s.Value)
                      .HasColumnName("Size")
                      .HasMaxLength(5)
                      .IsRequired();
                });

                svb.OwnsOne(sv => sv.Quantity, qb =>
                {
                    qb.Property(q => q.Value)
                      .HasColumnName("Quantity")
                      .IsRequired();
                });

                svb.OwnsOne(sv => sv.RestockThreshold, rb =>
                {
                    rb.Property(r => r.Value)
                      .HasColumnName("RestockThreshold")
                      .IsRequired();
                });
            });

            cvb.Navigation(cv => cv.SizeVariants).Metadata.SetField("_sizeVariants");
            cvb.Navigation(cv => cv.SizeVariants).UsePropertyAccessMode(PropertyAccessMode.Field);
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

        // Define the shadow property to enforce a unique constraint on UrlFriendlyName.Value
        builder.Property<string>("UrlFriendlyName_Value")
              .HasColumnName("UrlFriendlyName")
              .HasMaxLength(100)
              .IsRequired();

        // Set the unique constraint on the shadow property
        builder.HasIndex("UrlFriendlyName_Value").IsUnique();

        // Define the shadow property to enforce a unique constraint on UrlFriendlyName.Value
        builder.Property<string>("Name_Value")
              .HasColumnName("Name")
              .HasMaxLength(100)
              .IsRequired();

        // Set the unique constraint on the shadow property
        builder.HasIndex("Name_Value").IsUnique();



        builder.ComplexProperty(
            p => p.UrlFriendlyName, unb =>
            {
                unb.Property(n => n.Value)
                .HasColumnName(nameof(Product.UrlFriendlyName))
                .HasMaxLength(100)
                .IsRequired();
            });

        builder.Property(p => p.ShippingAndReturns)
            .HasColumnName(nameof(Product.ShippingAndReturns))
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Code)
            .HasColumnName(nameof(Product.Code))
            .HasMaxLength(50);


        builder.ComplexProperty(
            p => p.Description, desb =>
            {
                desb.Property(n => n.Value)
                .HasColumnName(nameof(Product.Description))
                .HasMaxLength(1000)
                .IsRequired();
            });

        builder.ComplexProperty(
          p => p.AverageRating, avb =>
          {
              avb.Property(av => av.Value)
                  .HasColumnName(nameof(Product.AverageRating));

              avb.Property(av => av.TotalRatingsCount)
                 .HasColumnName(nameof(Product.AverageRating.TotalRatingsCount));
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

        builder.Property(p => p.ProductType)
        .HasConversion(
            s => s.ToString(),
            dbStatus => (ProductType)Enum.Parse(typeof(ProductType), dbStatus));

        builder.Property(p => p.IsHandmade);
        builder.Property(p => p.OnReorder);

        builder.Property(p => p.MaterialId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => MaterialId.Of(dbId));

        builder.Property(p => p.CollectionId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => CollectionId.Of(dbId));

        builder.Property(p => p.BrandId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => BrandId.Of(dbId));

        builder.HasOne<Collection>()
            .WithMany()
            .HasForeignKey(p => p.CollectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Brand>()
            .WithMany()
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Material>()
            .WithMany()
            .HasForeignKey(p => p.MaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(o => o.Status)
            .HasDefaultValue(ProductStatus.Draft) // Default value
            .HasConversion(s => s.ToString(), // Converts enum to string before storing in the DB
                dbStatus => (ProductStatus)Enum.Parse(typeof(ProductStatus), dbStatus)) // Converts string back to enum when reading from DB
            .IsRequired(); // Ensure it's required

    }
}
