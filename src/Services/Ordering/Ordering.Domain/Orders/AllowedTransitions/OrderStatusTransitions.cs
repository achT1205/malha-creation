public static class OrderStatusTransitions
{
    private static readonly Dictionary<OrderStatus, List<OrderStatus>> _allowedTransitions = new()
    {
        // Initial States
        { OrderStatus.Draft, new List<OrderStatus> { OrderStatus.Pending, OrderStatus.Cancelled } },
        { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.PaymentPending, OrderStatus.Cancelled } },

        // Payment
        { OrderStatus.PaymentPending, new List<OrderStatus> { OrderStatus.Paid, OrderStatus.Cancelled, OrderStatus.Failed } },
        { OrderStatus.Paid, new List<OrderStatus> { OrderStatus.GracePeriodConfirmed, OrderStatus.Modified, OrderStatus.Cancelled } },

        // Grace Period and Modifications
        { OrderStatus.GracePeriodConfirmed, new List<OrderStatus> {  OrderStatus.Processing } },
        { OrderStatus.Modified, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.Cancelled } },

        // Preparation and Fulfillment
        { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.StockConfirmed, OrderStatus.Cancelled } },
        { OrderStatus.StockConfirmed, new List<OrderStatus> { OrderStatus.Packing, OrderStatus.Cancelled } },
        { OrderStatus.Packing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled } },
        { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.OutForDelivery, OrderStatus.Delivered, OrderStatus.Cancelled } },
        { OrderStatus.OutForDelivery, new List<OrderStatus> { OrderStatus.Delivered, OrderStatus.Cancelled } },
        { OrderStatus.AwaitingPickup, new List<OrderStatus> { OrderStatus.Delivered, OrderStatus.Cancelled } },
        { OrderStatus.Delivered, new List<OrderStatus> { OrderStatus.Completed, OrderStatus.Returned } },

        // Completion
        { OrderStatus.Completed, new List<OrderStatus>() },

        // Cancellation and Returns
        { OrderStatus.Cancelled, new List<OrderStatus>() },
        { OrderStatus.Returned, new List<OrderStatus> { OrderStatus.Refunding } },
        { OrderStatus.Refunding, new List<OrderStatus> { OrderStatus.Refunded } },
        { OrderStatus.Refunded, new List<OrderStatus>() },

        // Error States
        { OrderStatus.Failed, new List<OrderStatus>() },
        { OrderStatus.Rejected, new List<OrderStatus>() }
    };

    public static bool CanTransition(OrderStatus currentStatus, OrderStatus newStatus)
    {
        return _allowedTransitions.ContainsKey(currentStatus) &&
               _allowedTransitions[currentStatus].Contains(newStatus);
    }
}
