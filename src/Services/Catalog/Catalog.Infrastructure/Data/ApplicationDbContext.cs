

namespace Catalog.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Occasion> Occasions { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ColorVariant> ColorVariants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        // Configure TPH inheritance with a discriminator column
        modelBuilder.Entity<Product>()
            .HasDiscriminator<string>("ProductType")
            .HasValue<AccessoryProduct>("Accessory")
            .HasValue<ClothingProduct>("Clothing");

        // Mapping des propriétés communes à tous les produits (Product)
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => ProductId.Of(dbId));

            entity.ComplexProperty(
                     p => p.Name, nb =>
                     {
                         nb.Property(n => n.Value)
                             .HasColumnName(nameof(Product.Name))
                             .HasMaxLength(100)
                             .IsRequired();
                     });


            entity.ComplexProperty(
                   p => p.UrlFriendlyName, unb =>
                   {
                       unb.Property(n => n.Value)
                           .HasColumnName(nameof(Product.UrlFriendlyName))
                           .HasMaxLength(100)
                           .IsRequired();
                   });

            entity.ComplexProperty(
                    p => p.Description, desb =>
                    {
                        desb.Property(n => n.Value)
                            .HasColumnName(nameof(Product.Description))
                            .HasMaxLength(100)
                            .IsRequired();
                    });

            entity.ComplexProperty(
              p => p.AverageRating, avb =>
              {
                  avb.Property(av => av.Value)
                      .HasColumnName(nameof(Product.AverageRating))
                      .IsRequired();

                  avb.Property(av => av.TotalRatingsCount)
                     .HasColumnName(nameof(Product.AverageRating.TotalRatingsCount))
                     .IsRequired();
              });

            entity.ComplexProperty(
                  p => p.CoverImage, cib =>
                  {
                      cib.Property(ci => ci.ImageSrc)
                          .HasColumnName(nameof(Product.CoverImage.ImageSrc))
                          .IsRequired();

                      cib.Property(ci => ci.AltText)
                         .HasColumnName(nameof(Product.CoverImage.AltText))
                         .IsRequired();
                  });

            entity.Property(p => p.IsHandmade);

            entity.Property(p => p.ProductTypeId)
                .ValueGeneratedNever().HasConversion(
                id => id.Value,
                dbId => ProductTypeId.Of(dbId));

            entity.Property(p => p.MaterialId)
                .ValueGeneratedNever().HasConversion(
                id => id.Value,
                dbId => MaterialId.Of(dbId));

            entity.Property(p => p.CollectionId)
                .ValueGeneratedNever().HasConversion(
                id => id.Value,
                dbId => CollectionId.Of(dbId));

        });

        // Mapping spécifique à AccessoryProduct
        modelBuilder.Entity<AccessoryProduct>()
            .HasMany(a => a.ColorVariants)
            .WithOne()
            .HasForeignKey("ProductId");

        // Mapping spécifique à ClothingProduct
        modelBuilder.Entity<ClothingProduct>()
            .HasMany(c => c.ColorVariants)
            .WithOne()
            .HasForeignKey("ProductId");


        modelBuilder.Entity<ColorVariant>(entity =>
        {
            entity.HasKey(cv => cv.Id);

            entity.Property(cv => cv.Id)
              .ValueGeneratedNever()
              .HasConversion(
                  id => id.Value,
                  dbId => ColorVariantId.Of(dbId));

            entity.ComplexProperty(
             acv => acv.Color, cb =>
             {
                 cb.Property(c => c.Value)
                 .HasColumnName("Color")
                 .IsRequired()
                 .HasMaxLength(50);
             });

            entity.ComplexProperty(
             acv => acv.Slug, slb =>
             {
                 slb.Property(c => c.Value)
                   .HasColumnName("Slug")
                   .IsRequired()
                   .HasMaxLength(200);
             });

            entity.OwnsMany(cv => cv.Images, imsb =>
            {
                imsb.ToTable("ColorVariantImages");
            });
        });

        modelBuilder.Entity<AccessoryColorVariant>(entity =>
        {
            entity.ComplexProperty(
                acv => acv.Price, prb =>
                {
                    prb.Property(p => p.Currency)
                      .HasColumnName("Currency")
                      .IsRequired();

                    prb.Property(p => p.Amount)
                     .HasColumnName("Price")
                     .IsRequired();
                });

            entity.ComplexProperty(
                 acv => acv.Quantity, qb =>
                 {
                     qb.Property(q => q.Value)
                   .HasColumnName("Quantity")
                   .IsRequired();
                 });
        });


        modelBuilder.Entity<ClothingColorVariant>()
            .HasMany(c => c.SizeVariants)
            .WithOne()
            .HasForeignKey("ColorVariantId");

        modelBuilder.Entity<SizeVariant>(entity =>
        {
           entity.Property(sv => sv.Id)
          .ValueGeneratedNever()
          .HasConversion(
              id => id.Value,
              dbId => SizeVariantId.Of(dbId));

            entity.ComplexProperty(
             sv => sv.Price, svb =>
             {
                 svb.Property(p => p.Currency)
                  .HasColumnName("Currency")
                  .IsRequired();

                 svb.Property(p => p.Amount)
                 .HasColumnName("Price")
                 .IsRequired();
             });

            entity.ComplexProperty(
             sv => sv.Quantity, qb =>
             {
                 qb.Property(q => q.Value)
                   .HasColumnName("Quantity")
                   .IsRequired();
             });

            entity.ComplexProperty(
             sv => sv.Size, sb =>
             {
                 sb.Property(s => s.Value)
                   .HasColumnName("Size")
                   .IsRequired();
             });
        });
    }

    private void Productconfiguration(EntityTypeBuilder<Product> entityTypeBuilder, object builder)
    {
        throw new NotImplementedException();
    }

    Task<int> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
