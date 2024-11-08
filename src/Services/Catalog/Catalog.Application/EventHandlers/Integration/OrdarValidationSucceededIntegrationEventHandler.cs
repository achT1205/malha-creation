using Catalog.Application.Events.Integration;
using Catalog.Application.Products.Queries.GetProductForStockValidationById;
using Catalog.Application.Services;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.EventHandlers.Integration;

public class OrdarValidationSucceededIntegrationEventHandler
    (IPublishEndpoint publishEndpoint, ISender sender, IOrderingService orderingService, ILogger<OrdarValidationSucceededIntegrationEventHandler> logger)
    : IConsumer<OrdarValidationSucceededIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrdarValidationSucceededIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var orderId = context.Message.OrderId;
        var stockOrder = await orderingService.GetOrderByIdAsync(orderId);

        OrderStockRejectedDto orderStockRejectedDto =  new OrderStockRejectedDto() { OrderId = orderId };

        if (stockOrder != null)
        {
            foreach (var orderItem in stockOrder.OrderItems)
            {
                var result = await sender.Send(new GetProductForStockValidationByIdQuery(orderItem.ProductId));
                var product = result.Product;

                var cv = product.ColorVariants.FirstOrDefault(_ => _.Color == orderItem.Color.ToLower());
                var sv = cv.SizeVariants.FirstOrDefault(_ => _.Size.ToLower() == orderItem.Size.ToLower());
                var productCurrentStock = cv.Quantity.Value;
                if (product.ProductType == Catalog.Domain.Enums.ProductTypeEnum.Clothing)
                {
                    productCurrentStock = sv.Quantity;
                }

                if (productCurrentStock < orderItem.Quantity)
                {
                    orderStockRejectedDto.Items.Add(
                        new OrderStockRejectedItemDto(
                            product.Id, 
                            cv.Id, 
                            sv.Id, 
                            orderItem.Quantity,
                            productCurrentStock
                            )
                        );
                }
            }
            if (!orderStockRejectedDto.Items.Any())
            {
                var evt = new OrderStockConfirmedIntegrationEvent(orderId);
                await publishEndpoint.Publish(evt);
            }
            else {
                var evt = new OrderItemStockRejectedIntegrationEvent(orderStockRejectedDto);
                await publishEndpoint.Publish(evt); 
            }

        }
    }
}
