namespace BuildingBlocks.Messaging.Events;
public record ProductColorVariantPriceChangedEvent(Guid ProductId, Guid ColorVariantId, decimal Price, decimal OldPrice, string Currency) : IntegrationEvent;
