namespace Ordering.Application.Orders.Queries.GetOrdersByOrderCode;

public record GetOrderByOrderCodeQuery(string Code)
    : IQuery<GetOrderByOrderCodeResult>;

