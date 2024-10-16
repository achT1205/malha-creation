using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Interfaces;

public interface IImageRepository
{
    Task AddAsync(Image image);
    Task<Image> GetByIdAsync(ImageId id);
    Task<List<Image>> GetByIdsAsync(List<ImageId> ids);
    Task RemoveAsync(Image image);
    Task SaveChangesAsync();
}