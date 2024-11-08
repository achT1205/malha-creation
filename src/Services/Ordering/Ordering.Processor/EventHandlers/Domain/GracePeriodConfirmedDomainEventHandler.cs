using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Orders.Commands.RejectedOrder;
using Ordering.Application.Orders.Commands.ValidationOrder;
using Ordering.Application.Orders.Queries.GetOrdersById;
using Ordering.Domain.Orders.Events;
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


            IntegrationEvent evt = null;
            if (true)
            {
                var command = new ValidationOrderCommand(orderId);
                await sender.Send(command);

                await publishEndpoint.Publish (new OrdarValidationSucceededIntegrationEvent(orderId)); 
            }
            else
            {
                var command = new RejectOrderCommand(orderId);
                await sender.Send(command);

                await publishEndpoint.Publish(new OrdarValidationFailedIntegrationEvent(orderId));
            }
        }
    }
}
