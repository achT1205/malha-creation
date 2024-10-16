namespace Catalog.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Occasion> Occasions { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<AccessoryColorVariant> AccessoryColorVariants { get; set; }
    public DbSet<ClothingColorVariant> ClothingColorVariants { get; set; }
    public DbSet<ColorVariantBase> ColorVariants { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
