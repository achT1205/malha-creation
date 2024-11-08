using Ordering.Application.Orders.Commands.RejectedOrder;
using Ordering.Application.Orders.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class OrderItemStockRejectedIntegrationEventHandler
    (ISender sender, ILogger<OrderItemStockRejectedIntegrationEventHandler> logger)
    : IConsumer<OrderItemStockRejectedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderItemStockRejectedIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new RejectOrderCommand(context.Message.OrderStockRejected.OrderId);
        await sender.Send(command);
    }
}