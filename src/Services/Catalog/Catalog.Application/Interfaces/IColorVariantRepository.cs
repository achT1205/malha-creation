
namespace Catalog.Application.Interfaces;

public interface IColorVariantRepository
{
    Task AddAsync(ColorVariant ColorVariant);
    Task<List<ColorVariant>> GetAllAsync();
    Task<ColorVariant> GetByIdAsync(ColorVariantId id);
    Task RemoveAsync(ColorVariant ColorVariant);
    Task SaveChangesAsync();
    void UpdateAsync(ColorVariant colorVariant);
}

