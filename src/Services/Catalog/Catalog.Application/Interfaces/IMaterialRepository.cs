namespace Catalog.Application.Interfaces;
public interface IMaterialRepository
{
    Task<Material> GetByIdAsync(MaterialId id);
    Task<List<Material>> GetByIdsAsync(List<MaterialId> ids);
    Task AddAsync(Material material);
    Task RemoveAsync(Material material);
    Task SaveChangesAsync();
}
