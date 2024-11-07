using Cart.API.Cart.Commands.DeleteCart;
using MassTransit;
using ShoppingCart.API.Events.IntegrationEvent;

namespace ShoppingCart.API.EventHandlers.IntegrationEvents;

public class OrderStartedEventHander
    (ISender sender, ILogger<OrderStartedEventHander> logger)
    : IConsumer<OrderStartedEvent>
{
    public async Task Consume(ConsumeContext<OrderStartedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new DeleteCartCommand(context.Message.UserId);

        await sender.Send(command);
    }
}