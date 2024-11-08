using Ordering.Application.Orders.Commands.SetPaidOrderStatus;
using Ordering.Application.Orders.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class OrderPaymentSucceededIntegrationEventHandler
    (ISender sender, ILogger<OrderPaymentSucceededIntegrationEventHandler> logger)
    : IConsumer<OrderPaymentSucceededIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderPaymentSucceededIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new SetPaidOrderStatusCommand(context.Message.OrderId);
        await sender.Send(command);
    }
}