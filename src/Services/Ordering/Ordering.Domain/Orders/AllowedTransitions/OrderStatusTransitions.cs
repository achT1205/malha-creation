using Ordering.Domain.Orders.Enums;

namespace Ordering.Domain.Orders.AllowedTransitions;

public static class OrderStatusTransitions
{
    private static readonly Dictionary<OrderStatus, List<OrderStatus>> _allowedTransitions = new()
    {
        { OrderStatus.Draft, new List<OrderStatus> { OrderStatus.Pending, OrderStatus.Cancelled, OrderStatus.Deleted } },
        { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.GracePeriodConfirmed, OrderStatus.Cancelled, OrderStatus.Deleted } },
        { OrderStatus.GracePeriodConfirmed, new List<OrderStatus> { OrderStatus.Validated, OrderStatus.Rejected } },
        { OrderStatus.Validated, new List<OrderStatus> { OrderStatus.StockConfirmed } },
        { OrderStatus.StockConfirmed, new List<OrderStatus> { OrderStatus.Paid } },
        { OrderStatus.Paid, new List<OrderStatus> { OrderStatus.Shipped } },
        { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Completed } },
        { OrderStatus.Completed, new List<OrderStatus> { } }, // End state, no transitions out
        { OrderStatus.Cancelled, new List<OrderStatus> { } }, // End state, no transitions out
        { OrderStatus.Rejected, new List<OrderStatus> { } }, // End state, no transitions out
        { OrderStatus.Deleted, new List<OrderStatus> { } } // End state, no transitions out
    };

    public static bool CanTransition(OrderStatus currentStatus, OrderStatus newStatus)
    {
        return _allowedTransitions.ContainsKey(currentStatus) &&
               _allowedTransitions[currentStatus].Contains(newStatus);
    }
}
