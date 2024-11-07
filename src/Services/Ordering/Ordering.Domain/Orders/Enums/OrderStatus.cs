namespace Ordering.Domain.Orders.Enums;

public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Completed = 4,
    Cancelled = 5,
    Validated = 6,
    Paid = 7,
    StockConfirmed = 8,
    Rejected = 9,
    Shipped = 10,
    Deleted = 11,
    GracePeriodConfirmed = 12,
}