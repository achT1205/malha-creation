using Ordering.Application.Orders.Commands.RejectedOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class OrderPaymentFailedIntegrationEventHandler
    (ISender sender, ILogger<OrderPaymentFailedIntegrationEventHandler> logger)
    : IConsumer<OrderPaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<OrderPaymentFailedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new RejectOrderCommand(context.Message.OrderId);
        await sender.Send(command);
    }
}