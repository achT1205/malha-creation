

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

        modelBuilder.Entity<Product>().ToTable("Products");

        modelBuilder.Entity<Product>().HasKey(p => p.Id);

        modelBuilder.Entity<Product>().Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                dbId => ProductId.Of(dbId));

        modelBuilder.Entity<Product>().ComplexProperty(
                 p => p.Name, nb =>
                 {
                     nb.Property(n => n.Value)
                         .HasColumnName(nameof(Product.Name))
                         .HasMaxLength(100)
                         .IsRequired();
                 });


        modelBuilder.Entity<Product>().ComplexProperty(
               p => p.UrlFriendlyName, unb =>
               {
                   unb.Property(n => n.Value)
                       .HasColumnName(nameof(Product.UrlFriendlyName))
                       .HasMaxLength(100)
                       .IsRequired();
               });

        modelBuilder.Entity<Product>().ComplexProperty(
                p => p.Description, desb =>
                {
                    desb.Property(n => n.Value)
                        .HasColumnName(nameof(Product.Description))
                        .HasMaxLength(100)
                        .IsRequired();
                });

        modelBuilder.Entity<Product>().ComplexProperty(
          p => p.AverageRating, avb =>
          {
              avb.Property(av => av.Value)
                  .HasColumnName(nameof(Product.AverageRating))
                  .IsRequired();

              avb.Property(av => av.TotalRatingsCount)
                 .HasColumnName(nameof(Product.AverageRating.TotalRatingsCount))
                 .IsRequired();
          });

        modelBuilder.Entity<Product>().ComplexProperty(
              p => p.CoverImage, cib =>
              {
                  cib.Property(ci => ci.ImageSrc)
                      .HasColumnName(nameof(Product.CoverImage.ImageSrc))
                      .IsRequired();

                  cib.Property(ci => ci.AltText)
                     .HasColumnName(nameof(Product.CoverImage.AltText))
                     .IsRequired();
              });

        modelBuilder.Entity<Product>().Property(p => p.IsHandmade);

        modelBuilder.Entity<Product>().Property(p => p.ProductTypeId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => ProductTypeId.Of(dbId));

        modelBuilder.Entity<Product>().Property(p => p.MaterialId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => MaterialId.Of(dbId));

        modelBuilder.Entity<Product>().Property(p => p.CollectionId)
            .ValueGeneratedNever().HasConversion(
            id => id.Value,
            dbId => CollectionId.Of(dbId));


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

        // Mapping des variantes de couleur spécifiques à AccessoryProduct et ClothingProduct

        modelBuilder.Entity<ColorVariant>().ComplexProperty(
         acv => acv.Color, cb =>
         {
             cb.Property(c => c.Value)
             .HasColumnName("Color")
             .IsRequired()
             .HasMaxLength(50);
         });

        modelBuilder.Entity<ColorVariant>().ComplexProperty(
         acv => acv.Slug, slb =>
         {
             slb.Property(c => c.Value)
               .HasColumnName("Slug")
               .IsRequired()
               .HasMaxLength(200);
         });

        // Mapping spécifique à AccessoryProduct
        modelBuilder.Entity<ColorVariant>()
            .HasMany(a => a.Images)
            .WithOne();


        modelBuilder.Entity<AccessoryColorVariant>().ComplexProperty(
         acv => acv.Price, prb =>
            {
                prb.Property(p => p.Currency)
                 .HasColumnName("Currency")
                 .IsRequired();

                prb.Property(p => p.Amount)
                .HasColumnName("Price")
                .IsRequired();
            });

        modelBuilder.Entity<AccessoryColorVariant>().ComplexProperty(
         acv => acv.Quantity, qb =>
            {
                qb.Property(q => q.Value)
                  .HasColumnName("Quantity")
                  .IsRequired();
            });





        modelBuilder.Entity<ClothingColorVariant>()
            .HasMany(c => c.SizeVariants)
            .WithOne()
            .HasForeignKey("ColorVariantId");

        modelBuilder.Entity<SizeVariant>().ComplexProperty(
         acv => acv.Price, prb =>
         {
             prb.Property(p => p.Currency)
              .HasColumnName("Currency")
              .IsRequired();

             prb.Property(p => p.Amount)
             .HasColumnName("Price")
             .IsRequired();
         });

        modelBuilder.Entity<SizeVariant>().ComplexProperty(
         acv => acv.Quantity, qb =>
         {
             qb.Property(q => q.Value)
               .HasColumnName("Quantity")
               .IsRequired();
         });

        modelBuilder.Entity<SizeVariant>().ComplexProperty(
         acv => acv.Size, sb =>
         {
             sb.Property(s => s.Value)
               .HasColumnName("Quantity")
               .IsRequired();
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
