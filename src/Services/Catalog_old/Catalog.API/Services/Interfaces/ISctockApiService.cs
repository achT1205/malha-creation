namespace Catalog.API.Services.Interfaces;

public interface ISctockApiService
{
    Task<Stock?> GetStockByProductIdAsync(Guid productId);
}
