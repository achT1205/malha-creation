using Ordering.Domain.Orders.Enums;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection.Metadata;
using System.Threading.Channels;

namespace Ordering.Domain.Orders.AllowedTransitions;

public static class OrderStatusTransitions
{
    private static readonly Dictionary<OrderStatus, List<OrderStatus>> _allowedTransitions = new()
    {
        // User tunnel states
        { OrderStatus.Draft, new List<OrderStatus> { OrderStatus.Pending, OrderStatus.Deleted } },
        { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Confirmed, OrderStatus.Cancelled, OrderStatus.Deleted } },
        { OrderStatus.Confirmed, new List<OrderStatus> { OrderStatus.GracePeriodConfirmed, OrderStatus.Cancelled } },
        { OrderStatus.GracePeriodConfirmed, new List<OrderStatus> { OrderStatus.Validated, OrderStatus.Cancelled } },
        { OrderStatus.Paid, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.RefundRequested } },
        { OrderStatus.OutForDelivery, new List<OrderStatus> { OrderStatus.Delivered, OrderStatus.Returned } },
        { OrderStatus.Delivered, new List<OrderStatus> { OrderStatus.Completed, OrderStatus.RefundRequested, OrderStatus.Returned } },
        { OrderStatus.Completed, new List<OrderStatus> { } }, // Final state for the user
        { OrderStatus.Cancelled, new List<OrderStatus> { OrderStatus.Deleted } },
        { OrderStatus.RefundRequested, new List<OrderStatus> { OrderStatus.Refunded, OrderStatus.GracePeriodConfirmed } },
        { OrderStatus.Refunded, new List<OrderStatus> { OrderStatus.Deleted } },
        { OrderStatus.Returned, new List<OrderStatus> { OrderStatus.RefundRequested, OrderStatus.Completed } },

        // Order tunnel states
        { OrderStatus.Validated, new List<OrderStatus> { OrderStatus.Paid, OrderStatus.Failed, OrderStatus.Cancelled } },
        { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.StockConfirmed, OrderStatus.Cancelled } },
        { OrderStatus.StockConfirmed, new List<OrderStatus> { OrderStatus.Packing, OrderStatus.Rejected } },
        { OrderStatus.Packing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled } },
        { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.OutForDelivery, OrderStatus.Cancelled } },

        // Error and final states
        { OrderStatus.Failed, new List<OrderStatus> { OrderStatus.Deleted } },
        { OrderStatus.Rejected, new List<OrderStatus> { OrderStatus.Deleted } },
        { OrderStatus.Deleted, new List<OrderStatus> { } } // Final state
    };

    public static bool CanTransition(OrderStatus currentStatus, OrderStatus newStatus)
    {
        return _allowedTransitions.ContainsKey(currentStatus) &&
        _allowedTransitions[currentStatus].Contains(newStatus);
    }
}


//1. ** Initial Statuses and Draft**:
//   - Transitions from `Draft` to `Pending` are covered(`SubmitForProcessing`).
//   - Transition to `Deleted` from `Draft` is included in `Delete()`.

//2. ** User Confirmation Flow**:
//   - Transition from `Pending` to `Confirmed` is handled with `Confirm()`.
//   - Transition from `Confirmed` to `GracePeriodConfirmed` allows modifications(`EnterGracePeriod`).
//   - Transition from `GracePeriodConfirmed` to `Validated` is handled(`Validate()`).

//3. ** Payment and Processing Flow**:
//   - `Validated` to `Paid` (`MarkAsPaid()`) is included.
//   - Transition from `Paid` to `Processing` (`StartProcessing()`).
//   - Internal stock verification(`ConfirmStock()`), packing(`StartPacking()`), and shipping(`Ship()`) are covered.

//4. **Delivery and Completion**:
//   - `OutForDelivery` to `Delivered` transition (`Deliver()`).
//   - `Delivered` to `Completed` (`CompleteOrder()`).

//5. ** Cancellation and Deletion**:
//   - Transitions for cancellation(`Cancel()`) and deletion(`Delete()`) cover all allowed statuses.

//6. **Post-Delivery Scenarios**:
//   - Refund flow (`RequestRefund()` and `ProcessRefund()`).
//   - Return flow(`MarkAsReturned()`).

//### Missing Transitions

//The following transitions might be useful depending on your exact requirements:

//1. ** Pending -> Cancelled**: Ensure `Pending` orders can be cancelled.
//2. **GracePeriodConfirmed -> Cancelled**: Allow orders in the grace period to be cancelled if required.
//3. **Failed -> Deleted**: Transition to delete `Failed` orders if needed.