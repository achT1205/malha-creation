namespace Ordering.Application.Orders.Queries.GetOrdersByOrderCode;

public record GetOrdersByOrderCodeResult(IEnumerable<Order> Orders);