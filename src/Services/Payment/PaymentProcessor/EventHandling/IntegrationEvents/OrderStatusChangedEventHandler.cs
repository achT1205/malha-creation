using BuildingBlocks.Messaging.Events;
using MassTransit;
using PaymentProcessor.Events.IntegrationEvents;

namespace PaymentProcessor.EventHandling.IntegrationEvents;
public class OrderStatusChangedEventHandler
    (ILogger<OrderStatusChangedEventHandler> logger, IPublishEndpoint publishEndpoint)
    : IConsumer<OrderStatusChangedToStockConfirmedEvent>
{
    public async Task Consume(ConsumeContext<OrderStatusChangedToStockConfirmedEvent> context)
    {
        // TODO: Create new order and start order fullfillment process
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        IntegrationEvent orderPaymentIntegrationEvent;
        var status = context.Message.status;

        if (true)
        {
            orderPaymentIntegrationEvent = new OrderPaymentSucceeded(context.Message.OrderId);
        }
        else
        {
            orderPaymentIntegrationEvent = new OrderPaymentFailed(context.Message.OrderId);
        }

        await publishEndpoint.Publish(orderPaymentIntegrationEvent);


    }


}
