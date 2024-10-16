using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Interfaces;
public interface IProductTypeRepository
{
    Task<ProductType> GetByIdAsync(ProductTypeId id);
    Task<List<ProductType>> GetByIdsAsync(List<ProductTypeId> ids);
    Task AddAsync(ProductType productType);
    Task RemoveAsync(ProductType productType);
    Task SaveChangesAsync();
}

