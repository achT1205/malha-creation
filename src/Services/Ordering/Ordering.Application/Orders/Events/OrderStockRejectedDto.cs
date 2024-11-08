namespace Ordering.Application.Orders.Events;
public class OrderStockRejectedDto
{
    public Guid OrderId { get; set; }
    public List<OrderStockRejectedItemDto> Items { get; set; } = new();
};
public record OrderStockRejectedItemDto(Guid ProductId, Guid ColorVariantId, Guid SizeVariantId, int OrderQuqntity, int CurrentStock);
