//using Catalog.Application.Interfaces;
//using Catalog.Infrastructure.Data;

//namespace Catalog.Infrastructure.Repositories;
//public class ProductTypeRepository : IProductTypeRepository
//{
//    private readonly ApplicationDbContext _context;

//    public ProductTypeRepository(ApplicationDbContext context)
//    {
//        _context = context ?? throw new ArgumentNullException(nameof(context));
//    }

//    // Récupérer un type de produit par son ID
//    public async Task<ProductType> GetByIdAsync(ProductTypeId id)
//    {
//        return await _context.ProductTypes
//            .FirstOrDefaultAsync(productType => productType.Id == id)
//            ?? throw new KeyNotFoundException($"ProductType with ID {id.Value} not found.");
//    }

//    // Récupérer une liste de types de produits par leurs IDs
//    public async Task<List<ProductType>> GetByIdsAsync(List<ProductTypeId> ids)
//    {
//        return await _context.ProductTypes
//            .Where(productType => ids.Contains(productType.Id))
//            .ToListAsync();
//    }

//    // Ajouter un nouveau type de produit
//    public async Task AddAsync(ProductType productType)
//    {
//        if (productType == null)
//        {
//            throw new ArgumentNullException(nameof(productType));
//        }
//        await _context.ProductTypes.AddAsync(productType);
//    }

//    // Supprimer un type de produit
//    public async Task RemoveAsync(ProductType productType)
//    {
//        if (productType == null)
//        {
//            throw new ArgumentNullException(nameof(productType));
//        }
//        _context.ProductTypes.Remove(productType);
//    }

//    // Sauvegarder les changements dans la base de données
//    public async Task SaveChangesAsync()
//    {
//        await _context.SaveChangesAsync();
//    }
//}

