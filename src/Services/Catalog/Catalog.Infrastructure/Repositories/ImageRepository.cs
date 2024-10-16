using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _context;

    public ImageRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Récupérer une image par son ID
    public async Task<Image> GetByIdAsync(ImageId id)
    {
        return await _context.Images
            .FirstOrDefaultAsync(image => image.Id == id)
            ?? throw new KeyNotFoundException($"Image with ID {id.Value} not found.");
    }

    // Récupérer une liste d'images par leurs IDs
    public async Task<List<Image>> GetByIdsAsync(List<ImageId> ids)
    {
        return await _context.Images
            .Where(image => ids.Contains(image.Id))
            .ToListAsync();
    }

    // Ajouter une nouvelle image
    public async Task AddAsync(Image image)
    {
        if (image == null)
        {
            throw new ArgumentNullException(nameof(image));
        }
        await _context.Images.AddAsync(image);
    }

    // Supprimer une image
    public Task RemoveAsync(Image image)
    {
        if (image == null)
        {
            throw new ArgumentNullException(nameof(image));
        }
        _context.Images.Remove(image);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
