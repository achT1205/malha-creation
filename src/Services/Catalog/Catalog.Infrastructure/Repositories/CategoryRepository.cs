using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Récupérer une catégorie par son ID
    public async Task<Category> GetByIdAsync(CategoryId id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id)
            ?? throw new KeyNotFoundException($"Category with ID {id.Value} not found.");
    }

    // Récupérer une liste de catégories par leurs IDs
    public async Task<List<Category>> GetByIdsAsync(List<CategoryId> ids)
    {
        return await _context.Categories
            .Where(category => ids.Contains(category.Id))
            .ToListAsync();
    }

    // Ajouter une nouvelle catégorie
    public async Task AddAsync(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        await _context.Categories.AddAsync(category);
    }

    // Supprimer une catégorie
    public async Task RemoveAsync(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        _context.Categories.Remove(category);
    }

    // Sauvegarder les changements dans la base de données
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

