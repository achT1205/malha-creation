namespace Ordering.Application.Orders.Queries.GetOrdersByOrderCode;

public record GetOrdersByOrderCodeResult(IEnumerable<OrderDto> Orders);