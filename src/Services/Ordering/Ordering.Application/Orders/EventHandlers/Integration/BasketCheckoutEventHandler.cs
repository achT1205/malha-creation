using Ordering.Application.Orders.Commands.CreateOrder;
namespace Ordering.Application.Orders.EventHandlers.Integration;
public class BasketCheckoutEventHandler
    (
    ISender sender,
    ILogger<BasketCheckoutEventHandler> logger,
    ICartService cartService)
    : IConsumer<CartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = await MapToCreateOrderCommand(context.Message);
        await sender.Send(command);


    }

    private async Task<AutoCreateOrderCommand> MapToCreateOrderCommand(CartCheckoutEvent message)
    {
        // Create full order with incoming event data
        var address = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.City, message.ZipCode);
        var payment = new PaymentDto(message.CardHolderName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);

        var cart = await cartService.GetCartByUserIdAsync(message.UserId);

        var orderItems = cart?.Items.Select(cartItem => new OrderItemDto(
            cartItem.ProductId,
            cartItem.ProductName,
            cartItem.Quantity,
            cartItem.Color,
            cartItem.Size,
            cartItem.Price,
            cartItem.Slug
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
