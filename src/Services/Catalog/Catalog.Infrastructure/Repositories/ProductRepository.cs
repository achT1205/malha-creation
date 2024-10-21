using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    //Récupérer un produit par son ID
    public async Task<Product> GetByIdAsync(ProductId id)
    {
        var product = await _context.Products
        .Include(p => ((AccessoryProduct)p).ColorVariants)
            .ThenInclude(cv => cv.Images)
        .Include(p => ((ClothingProduct)p).ColorVariants)
            .ThenInclude(cv => cv.Images)
        .Include(p => ((ClothingProduct)p).ColorVariants)
            .ThenInclude(cv => (cv as ClothingColorVariant).SizeVariants)
        .FirstOrDefaultAsync(p => p.Id == id);

        return product ?? throw new KeyNotFoundException($"Product with ID {id.Value} not found.");
        //return await _context.Products

        //    .Include(p => p.ColorVariants) 
        //        .ThenInclude(cv => cv.Images) 
        //    .Include(p => p.ColorVariants) 
        //        .ThenInclude(cv => (cv).SizeVariants) 
        //    .FirstOrDefaultAsync(product => product.Id == id)
        //    ?? throw new KeyNotFoundException($"Product with ID {id.Value} not found.");
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await _context.Products
        .Include(p => ((AccessoryProduct)p).ColorVariants)
            .ThenInclude(cv => cv.Images)
        .Include(p => ((ClothingProduct)p).ColorVariants)
            .ThenInclude(cv => cv.Images)
        .Include(p => ((ClothingProduct)p).ColorVariants)
            .ThenInclude(cv => (cv as ClothingColorVariant).SizeVariants)
        .FirstOrDefaultAsync(p => p.ColorVariants.Any(cv => cv.Slug.Value == slug));

        //return await _context.Products
        //    .Include(p => p.ColorVariants)
        //        .ThenInclude(cv => cv.Images)
        //    .Include(p => p.ColorVariants)
        //        .ThenInclude(cv => (cv).SizeVariants)
        //    .FirstOrDefaultAsync(p => p.ColorVariants.Any(cv => cv.Slug.Value == slug));
    }

    // Récupérer tous les produits
    public async Task<List<Product>> GetAllAsync()
    {
        //return await _context.Products
        //    .Include(p => p.ColorVariants)
        //        .ThenInclude(cv => cv.Images)
        //    .Include(p => p.ColorVariants)
        //        .ThenInclude(cv => (cv).SizeVariants)
        //    .ToListAsync();

        return await _context.Products
            .Include(p => ((AccessoryProduct)p).ColorVariants)
                .ThenInclude(cv => cv.Images)
            .Include(p => ((ClothingProduct)p).ColorVariants)
                .ThenInclude(cv => cv.Images)
            .Include(p => ((ClothingProduct)p).ColorVariants)
                .ThenInclude(cv => (cv as ClothingColorVariant).SizeVariants)
            .ToListAsync();
    }
    // Ajouter un nouveau produit
    public async Task AddAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        await _context.Products.AddAsync(product);
    }

    // Mettre à jour un produit existant
    public async Task UpdateAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        _context.Products.Update(product);
    }

    // Supprimer un produit
    public async Task RemoveAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        _context.Products.Remove(product);
    }

    // Sauvegarder les changements dans la base de données
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
