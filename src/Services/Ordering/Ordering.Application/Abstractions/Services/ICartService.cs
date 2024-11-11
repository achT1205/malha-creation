namespace Ordering.Application.Abstractions.Services;

public interface ICartService
{
    Task<Basket?> GetCartByUserIdAsync(Guid productId);
}
