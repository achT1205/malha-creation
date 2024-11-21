using Ordering.Application.Orders.Commands.CreateOrder;
namespace Ordering.Application.Orders.EventHandlers.Integration;
public class BasketCheckoutEventHandler
    (
    ISender sender,
    ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<CartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {

        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var order = CreateNewOrder(context.Message);
        var command = new AutoCreateOrderCommand(order);
        await sender.Send(command);
    }


    private Order CreateNewOrder(CartCheckoutEvent message)
    {

        var shippingAddress = message.ShippingAddress;
        var billingAddress = message.BillingAddress;
        var payment = message.Payment;
        var basket = message.Basket;


        var sAdd = Ordering.Domain.ValueObjects.Address.Of(shippingAddress.FirstName, shippingAddress.LastName, shippingAddress.EmailAddress, shippingAddress.AddressLine, shippingAddress.Country, shippingAddress.City, shippingAddress.ZipCode);
        var bAdd = Ordering.Domain.ValueObjects.Address.Of(billingAddress.FirstName, billingAddress.LastName, billingAddress.EmailAddress, billingAddress.AddressLine, billingAddress.Country, billingAddress.City, billingAddress.ZipCode);
        var orderId = Guid.NewGuid();
        var newOrder = Order.Create(
                id: OrderId.Of(orderId),
                customerId: CustomerId.Of(basket.UserId),
                OrderCode: OrderCode.Of(OrderCodeGenerator.GenerateOrderCode(basket.UserId)),
                shippingAddress: sAdd,
                billingAddress: bAdd,
                payment: Ordering.Domain.ValueObjects.Payment.Of(payment.CardHolderName, payment.CardNumber, payment.Expiration, payment.Cvv, payment.PaymentMethod)
                );

        foreach (var orderItemDto in basket.Items)
        {
            newOrder.Add(
                OrderId.Of(orderId),
                ProductId.Of(orderItemDto.ProductId),
                orderItemDto.Quantity,
                orderItemDto.Price,
                orderItemDto.Color,
                orderItemDto.Size,
                orderItemDto.ProductName,
                orderItemDto.Slug,
                orderItemDto.Coupon?.Amount,
                orderItemDto.Coupon?.Description
                );
        }
        return newOrder;
    }
}
