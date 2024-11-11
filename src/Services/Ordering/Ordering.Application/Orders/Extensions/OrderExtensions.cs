namespace Ordering.Application.Orders.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderCode: order.OrderCode.Value,
            ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.City, order.ShippingAddress.ZipCode),
            BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.City, order.BillingAddress.ZipCode),
            Payment: new PaymentDto(order.Payment.CardHolderName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
            Status: order.Status,
            TotalPrice: order.TotalPrice,
            OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.ProductId.Value, oi.ProductName, oi.Quantity, oi.Color, oi.Size, oi.Price, oi.Slug)).ToList()
        ));
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    public static OrderStockDto ToOrderStockDto(this Order order)
    {
        return OrderStockDtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderCode: order.OrderCode.Value,
                    ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.City, order.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.City, order.BillingAddress.ZipCode),
                    Payment: new PaymentDto(order.Payment.CardHolderName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                    Status: order.Status,
                    TotalPrice: order.TotalPrice,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.ProductId.Value, oi.ProductName, oi.Quantity, oi.Color, oi.Size, oi.Price, oi.Slug)).ToList()
                );
    }


    private static OrderStockDto OrderStockDtoFromOrder(Order order)
    {
        return new OrderStockDto(
            Id: order.Id.Value,
            OrderItems: order.OrderItems.Select(oi => new OrderItemStockDto(oi.ProductId.Value, oi.Quantity, oi.Color, oi.Size)).ToList()
            );
    }
}