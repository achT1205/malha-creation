﻿using Ordering.Application.Orders.Commands.RejectedOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class OrderItemStockRejectedEventHandler
    (ISender sender, ILogger<OrderItemStockRejectedEventHandler> logger)
    : IConsumer<OrderItemStockRejectedEvent>
{
    public async Task Consume(ConsumeContext<OrderItemStockRejectedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new OrderItemStockRejectCommand(context.Message.OrderStockRejected);
        await sender.Send(command);
    }
}