using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Orders.Queries.GetOrdersById;
using Ordering.Processor.Events.Domain;
using Ordering.Processor.Events.Intetration;

namespace Ordering.Processor.EventHandlers.Domain;


public class GracePeriodConfirmedDomainEventHandler
    (ISender sender, IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<GracePeriodConfirmedDomainEventHandler> logger)
    : INotificationHandler<GracePeriodConfirmedDomainEvent>
{
    public async Task Handle(GracePeriodConfirmedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderId = domainEvent.OrderId;
            var result = await sender.Send(new GetOrdersByIdQuery(orderId));

            // implemete the validation here  result.Order

            var evt = new OrdarValidateEventIntegration(orderId);

            await publishEndpoint.Publish(evt, cancellationToken);
        }
    }
}
