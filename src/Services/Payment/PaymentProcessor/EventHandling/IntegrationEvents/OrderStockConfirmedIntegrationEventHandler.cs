using BuildingBlocks.Messaging.Events;
using MassTransit;
using PaymentProcessor.Events.IntegrationEvents;

namespace PaymentProcessor.EventHandling.IntegrationEvents;
public class OrderStockConfirmedIntegrationEventHandler
    (ILogger<OrderStockConfirmedIntegrationEventHandler> logger, IPublishEndpoint publishEndpoint)
    : IConsumer<OrderStockConfirmedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderStockConfirmedIntegrationEvent> context)
    {
        // TODO: Create new order and start order fullfillment process
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        IntegrationEvent orderPaymentIntegrationEvent;

        if (true)
        {
            orderPaymentIntegrationEvent = new OrderPaymentSucceededIntegrationEvent(context.Message.OrderId);
        }
        else
        {
            orderPaymentIntegrationEvent = new OrderPaymentFailedIntegrationEvent(context.Message.OrderId);
        }

        await publishEndpoint.Publish(orderPaymentIntegrationEvent);


    }


}
