namespace Cart.API.Services.Interfaces;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(Guid productId);
}