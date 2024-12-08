using Ordering.Application.Orders.Commands.ConfirmOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class OrderStockConfirmedEventHandler
    (ISender sender, ILogger<OrderStockConfirmedEventHandler> logger)
    : IConsumer<OrderStockConfirmedEvent>
{
    public async Task Consume(ConsumeContext<OrderStockConfirmedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new ConfirmStockCommand(context.Message.OrderId);
        await sender.Send(command);
    }
}