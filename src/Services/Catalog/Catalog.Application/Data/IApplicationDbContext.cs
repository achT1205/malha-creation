namespace Catalog.Application.Data;
public interface IApplicationDbContext
{
    DbSet<AccessoryColorVariant> AccessoryColorVariants { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<ClothingColorVariant> ClothingColorVariants { get; set; }
    DbSet<Collection> Collections { get; set; }
    DbSet<ColorVariantBase> ColorVariants { get; set; }
    DbSet<Image> Images { get; set; }
    DbSet<Material> Materials { get; set; }
    DbSet<Occasion> Occasions { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductType> ProductTypes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}