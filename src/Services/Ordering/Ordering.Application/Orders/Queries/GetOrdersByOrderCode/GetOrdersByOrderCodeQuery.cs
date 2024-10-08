namespace Ordering.Application.Orders.Queries.GetOrdersByOrderCode;

public record GetOrdersByOrderCodeQuery(string Code)
    : IQuery<GetOrdersByOrderCodeResult>;

