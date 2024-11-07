using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Récupérer une catégorie par son ID
    public async Task<Brand> GetByIdAsync(BrandId id)
    {
        return await _context.Brands
            .FirstOrDefaultAsync(Brand => Brand.Id == id)
            ?? throw new KeyNotFoundException($"Brand with ID {id.Value} not found.");
    }

    // Récupérer une liste de catégories par leurs IDs
    public async Task<List<Brand>> GetByIdsAsync(List<BrandId> ids)
    {
        return await _context.Brands
            .Where(Brand => ids.Contains(Brand.Id))
            .ToListAsync();
    }

    // Ajouter une nouvelle catégorie
    public async Task AddAsync(Brand Brand)
    {
        if (Brand == null)
        {
            throw new ArgumentNullException(nameof(Brand));
        }
        await _context.Brands.AddAsync(Brand);
    }

    // Supprimer une catégorie
    public async Task RemoveAsync(Brand Brand)
    {
        if (Brand == null)
        {
            throw new ArgumentNullException(nameof(Brand));
        }
        _context.Brands.Remove(Brand);
    }

    // Sauvegarder les changements dans la base de données
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        return await _context.Brands.ToListAsync();
    }
}

