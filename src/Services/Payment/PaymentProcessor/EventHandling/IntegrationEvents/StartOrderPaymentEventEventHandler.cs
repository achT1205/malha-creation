using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace PaymentProcessor.EventHandling.IntegrationEvents;
public class StartOrderPaymentEventEventHandler(ILogger<StartOrderPaymentEventEventHandler> logger, IPublishEndpoint publishEndpoint) : IConsumer<StartOrderPaymentEvent>
{
    public async Task Consume(ConsumeContext<StartOrderPaymentEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        //IntegrationEvent orderPaymentIntegrationEvent;
        //// implement payment here
        //if (true)
        //{
        //    orderPaymentIntegrationEvent = new OrderPaymentSucceededEvent(context.Message.OrderId);
        //}
        //else
        //{
        //    orderPaymentIntegrationEvent = new OrderPaymentFailedEvent(context.Message.OrderId);
        //}

        await publishEndpoint.Publish(new OrderPaymentSucceededEvent(context.Message.OrderId));
    }
}
