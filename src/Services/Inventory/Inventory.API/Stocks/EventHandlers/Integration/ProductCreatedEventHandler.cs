using BuildingBlocks.Messaging.Events;
using Inventory.API.Stocks.Commands.CreateStock;
using MassTransit;

namespace Inventory.API.Stocks.EventHandlers.Integration;
public class ProductCreatedEventHandler(ISender sender, ILogger<ProductCreatedEventHandler> logger)
    : IConsumer<ProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {

        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        try
        {
            var command = MapToCreateProductCommand(context.Message);
            await sender.Send(command);
        }
        catch (Exception ex)
        {
            throw new ProductCreatedEventHandlerException(ex.Message);
        }
    }

    private CreateStockAutoCommand MapToCreateProductCommand(ProductCreatedEvent message)
    {
        var stock = new StockDto
        {
            ProductId = message.Id,
            ProductType = message.ProductType,
            ColorVariants = new List<ColorVariant>()
        };

        if (message.ProductType.ToLower() == "clothing")
        {
            foreach (var cv in message.ColorVariants)
            {
                if (cv.Sizes != null)
                    foreach (var size in cv.Sizes)
                    {
                        stock.ColorVariants.Add(
                             new ColorVariant
                             {
                                 Color = cv.Color,
                                 Quantity = size.Quantity,
                                 Size = size.Size,
                             });
                    }

            }
        }
        else
        {
            stock.ColorVariants = message.ColorVariants.Select(cv => new ColorVariant
            {
                Color = cv.Color,
                Quantity = cv.Quantity.Value,
            }).ToList();
        }
        return new CreateStockAutoCommand
        {
            ProductId = stock.ProductId,
            ProductType = stock.ProductType,
            ColorVariants = stock.ColorVariants
        };
    }
}