namespace Catalog.Application.Services;

public record OrderStockDto(
    Guid Id,
    List<OrderItemStockDto> OrderItems);
public record OrderItemStockDto(Guid ProductId, int Quantity, string Color, string Size);

