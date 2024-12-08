namespace Catalog.Application.Services;

public interface IOrderingService
{
    Task<OrderStockDto?> GetOrderByIdAsync(Guid orderId);
}
