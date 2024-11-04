namespace Catalog.Application.Data;
public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Collection> Collections { get; set; }
    DbSet<ColorVariant> ColorVariants { get; set; }
    DbSet<Material> Materials { get; set; }
    DbSet<Occasion> Occasions { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductType> ProductTypes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}