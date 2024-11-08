using Catalog.Application.Events.Integration;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.EventHandlers.Integration;

public class OrdarValidateEventIntegrationHandler
    (ISender sender, ILogger<OrdarValidateEventIntegrationHandler> logger)
    : IConsumer<OrdarValidateEventIntegration>
{
    public async Task Consume(ConsumeContext<OrdarValidateEventIntegration> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var orderId = context.Message.OrderId;

        // implament order service 
        // get order
        // check order items stock availablelity with the product ons 
        // implement order stock confirmed IntegrationEvent
       // await sender.Send(command);
    }

   
}
