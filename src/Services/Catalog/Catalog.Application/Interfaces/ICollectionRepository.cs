using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Interfaces;
public interface ICollectionRepository
{
    Task<Collection> GetByIdAsync(CollectionId id);
    Task<List<Collection>> GetByIdsAsync(List<CollectionId> ids);
    Task AddAsync(Collection collection);
    Task RemoveAsync(Collection collection);
    Task<List<Collection>> GetAllAsync();
    Task SaveChangesAsync();
}
