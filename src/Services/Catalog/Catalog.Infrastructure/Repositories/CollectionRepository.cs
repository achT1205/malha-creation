using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly ApplicationDbContext _context;

    public CollectionRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Récupérer une collection par son ID
    public async Task<Collection> GetByIdAsync(CollectionId id)
    {
        return await _context.Collections
            .FirstOrDefaultAsync(collection => collection.Id == id)
            ?? throw new KeyNotFoundException($"Collection with ID {id.Value} not found.");
    }

    // Récupérer une liste de collections par leurs IDs
    public async Task<List<Collection>> GetByIdsAsync(List<CollectionId> ids)
    {
        return await _context.Collections
            .Where(collection => ids.Contains(collection.Id))
            .ToListAsync();
    }

    // Ajouter une nouvelle collection
    public async Task AddAsync(Collection collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }
        await _context.Collections.AddAsync(collection);
    }

    // Supprimer une collection
    public async Task RemoveAsync(Collection collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }
        _context.Collections.Remove(collection);
    }

    // Sauvegarder les changements dans la base de données
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Collection>> GetAllAsync()
    {
        return await _context.Collections.ToListAsync();
    }
}

