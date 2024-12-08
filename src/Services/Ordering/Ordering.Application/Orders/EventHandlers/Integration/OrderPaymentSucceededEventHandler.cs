namespace Ordering.Application.Orders.EventHandlers.Integration;

public class OrderPaymentSucceededEventHandler
    (ISender sender, ILogger<OrderPaymentSucceededEventHandler> logger)
    : IConsumer<OrderPaymentSucceededEvent>
{
    public async Task Consume(ConsumeContext<OrderPaymentSucceededEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

    }
}