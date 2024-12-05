using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class ColorVariantRepository : IColorVariantRepository
{
    private readonly ApplicationDbContext _context;

    public ColorVariantRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public Task AddAsync(ColorVariant ColorVariant)
    {
        throw new NotImplementedException();
    }

    public Task<List<ColorVariant>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ColorVariant> GetByIdAsync(ColorVariantId id)
    {
        return await _context.ColorVariants
           .FirstOrDefaultAsync(_ => _.Id == id)
           ?? throw new KeyNotFoundException($"ColorVariant with ID {id.Value} not found.");
    }

    public void UpdateAsync(ColorVariant colorVariant)
    {
        if (colorVariant == null)
        {
            throw new ArgumentNullException(nameof(colorVariant));
        }
        _context.ColorVariants.Update(colorVariant);
    }

    public Task RemoveAsync(ColorVariant ColorVariant)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
