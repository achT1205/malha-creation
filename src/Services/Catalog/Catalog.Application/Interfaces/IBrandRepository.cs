

namespace Catalog.Application.Interfaces;

public interface IBrandRepository
{
    Task<Brand> GetByIdAsync(BrandId id);
    Task<List<Brand>> GetByIdsAsync(List<BrandId> ids);
    Task AddAsync(Brand Brand);
    Task RemoveAsync(Brand Brand);
    Task<List<Brand>> GetAllAsync();
    Task SaveChangesAsync();
}

