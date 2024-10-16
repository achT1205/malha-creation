using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Interfaces;
public interface IOccasionRepository
{
    Task<Occasion> GetByIdAsync(OccasionId id);
    Task<List<Occasion>> GetByIdsAsync(List<OccasionId> ids);
    Task AddAsync(Occasion occasion);
    Task RemoveAsync(Occasion occasion);
    Task SaveChangesAsync();
}
