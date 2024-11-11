using Catalog.Application.Products.Queries.GetProductForStockValidationById;

namespace Catalog.Application.EventHandlers.Integrations;
public class OrderValidationSucceededEventHandler
    (IPublishEndpoint publishEndpoint, ISender sender, IOrderingService orderingService, ILogger<OrderValidationSucceededEventHandler> logger)
    : IConsumer<OrderValidationSucceededEvent>
{
    public async Task Consume(ConsumeContext<OrderValidationSucceededEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var orderId = context.Message.OrderId;
        var stockOrder = await orderingService.GetOrderByIdAsync(orderId);

        OrderStockRejectedDto orderStockRejectedDto = new OrderStockRejectedDto() { OrderId = orderId };

        if (stockOrder != null)
        {
            foreach (var orderItem in stockOrder.OrderItems)
            {
                var result = await sender.Send(new GetProductForStockValidationByIdQuery(orderItem.ProductId));
                var product = result.Product;

                var cv = product.ColorVariants.FirstOrDefault(_ => _.Color.ToLower() == orderItem.Color.ToLower());
                var sv = cv?.SizeVariants.FirstOrDefault(_ => _.Size.ToLower() == orderItem.Size.ToLower());
                var productCurrentStock = cv.Quantity.HasValue ? cv.Quantity.Value : 0;
                if (product.ProductType == ProductType.Clothing)
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
                var evt = new OrderStockConfirmedEvent(orderId);
                await publishEndpoint.Publish(evt);
            }
            else
            {
                var evt = new OrderItemStockRejectedEvent(orderStockRejectedDto);
                await publishEndpoint.Publish(evt);
            }

        }
    }
}
