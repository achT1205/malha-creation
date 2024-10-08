
namespace Ordering.Infrastructure.Services
{
    public interface IProductService
    {
        Task<object?> GetProduct(string productId);
    }
}