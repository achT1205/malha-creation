public enum OrderStatus
{
    // Initial States
    Draft,              // The order is being created but not yet submitted.
    Pending,            // The order has been submitted, validated, and is ready for further processing.

    // Payment
    PaymentPending,     // Waiting for payment to be completed.
    Paid,               // Payment has been successfully completed.

    // Grace Period and Modifications
    GracePeriodConfirmed, // The grace period is active, and the order can still be modified or canceled.
    Modified,           // The order has been updated during the grace period.

    // Preparation and Fulfillment
    Processing,         // The order is being prepared for shipment (e.g., picking items).
    StockConfirmed,     // Stock has been confirmed and reserved for the order.
    Packing,            // The order is being packed for shipment.
    Shipped,            // The order has been shipped to the customer.
    OutForDelivery,     // The order is out for final delivery to the customer.
    AwaitingPickup,     // The order is ready for pickup (e.g., in-store or curbside pickup).
    Delivered,          // The order has been delivered to the customer.

    // Completion
    Completed,          // The order has been fully processed and is considered finalized.

    // Cancellation and Returns
    Cancelled,          // The order has been canceled before processing.
    Returned,           // The order has been returned after delivery.
    Refunding,          // Order is in the process of being refunded.
    Refunded,

    // Error States
    Failed,             // The order failed due to issues (e.g., payment failure or stock unavailability).
    Rejected            // The order was rejected (e.g., fraud detection, invalid details).
}
