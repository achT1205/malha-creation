namespace ShoppingCart.API.Events.IntegrationEvent;

public record class OrderStartedEvent(Guid UserId) : BuildingBlocks.Messaging.Events.IntegrationEvent;
