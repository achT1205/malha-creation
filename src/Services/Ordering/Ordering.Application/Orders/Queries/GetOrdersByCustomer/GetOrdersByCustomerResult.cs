namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;
public record GetOrdersByCustomerResult(IEnumerable<Order> Orders);