namespace Ordering.Domain.Orders.Enums;

public enum OrderStatus
{
    Draft,                // The order is created but not yet submitted
    Pending,              // Order is submitted and awaiting further action
    Confirmed,            // Order has been confirmed
    Validated,            // Order has passed initial validation, such as payment confirmation
    Paid,                 // Payment has been received
    Processing,           // Order is being processed for shipment
    StockConfirmed,       // Items are reserved in stock
    Packing,              // Order is being packed for shipment
    Shipped,              // Order has been dispatched and is in transit
    OutForDelivery,       // Order is out for final delivery to customer
    Delivered,            // Order has been delivered to the customer
    Completed,            // Order is complete, no further action required
    Cancelled,            // Order has been cancelled by the customer or system
    RefundRequested,      // Customer has requested a refund
    Refunded,             // Refund has been processed
    Returned,             // Order has been returned and received by the seller
    Failed,               // Order could not be processed due to an error
    Rejected,             // Order was rejected, typically due to validation issues or stock unavailability
    GracePeriodConfirmed, // Order is confirmed after a grace period
    Deleted               // Order has been removed from the system
}