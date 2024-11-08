using Ordering.Application.Orders.Commands.RejectedOrder;
using Ordering.Application.Orders.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class OrderPaymentFailedIntegrationEventHandler
    (ISender sender, ILogger<OrderPaymentFailedIntegrationEventHandler> logger)
    : IConsumer<OrderPaymentFailedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderPaymentFailedIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new RejectOrderCommand(context.Message.OrderId);
        await sender.Send(command);
    }
}