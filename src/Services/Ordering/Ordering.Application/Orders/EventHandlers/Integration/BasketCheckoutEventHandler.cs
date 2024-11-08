using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<CartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {
        // TODO: Create new order and start order fullfillment process
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private AutoCreateOrderCommand MapToCreateOrderCommand(CartCheckoutEvent message)
    {
        // Create full order with incoming event data
        var address = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var payment = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderItems = message.Cart.Items.Select(cartItem => new OrderItemDto(
                 cartItem.ProductId,
                 cartItem.Quantity,
                 cartItem.Color,
                 cartItem.Size,
                 cartItem.Price
        )).ToList();


        return new AutoCreateOrderCommand()
        {
            CustomerId = message.UserId,
            BillingAddress = address,
            ShippingAddress = address,
            Payment = payment,
            OrderItems = orderItems
        };
    }
}
