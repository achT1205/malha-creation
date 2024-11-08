

namespace Catalog.Application.EventHandlers.Domain;

public class ProductColorVariantPriceChangedDomainEventHandler
    (IPublishEndpoint publishEndpoint, ILogger<ProductColorVariantPriceChangedDomainEventHandler> logger)
    : INotificationHandler<ProductColorVariantPriceChangedDomainEvent>
{
    public async Task Handle(ProductColorVariantPriceChangedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var evt = domainEvent.Adapt<ProductColorVariantPriceChangedEvent>();
        await publishEndpoint.Publish(evt, cancellationToken);
    }
}