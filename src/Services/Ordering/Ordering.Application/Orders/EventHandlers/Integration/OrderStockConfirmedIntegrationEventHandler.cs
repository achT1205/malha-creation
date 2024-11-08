using Ordering.Application.Orders.Commands.ConfirmOrder;
using Ordering.Application.Orders.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class OrderStockConfirmedIntegrationEventHandler
    (ISender sender, ILogger<OrderStockConfirmedIntegrationEventHandler> logger)
    : IConsumer<OrderStockConfirmedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderStockConfirmedIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new ConfirmStockCommand(context.Message.OrderId);
        await sender.Send(command);
    }
}