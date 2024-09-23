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

        var command = MapToCreateProductCommand(context.Message);
        await sender.Send(command);

        throw new NotImplementedException();
    }

    private CreateStockCommand MapToCreateProductCommand(ProductCreatedEvent message)
    {
        var stock = new Stock { ProductId = message.Id };

        if (message.ProductType.ToLower() == "clothing")
        {
            stock.ColorVariants = (List<ColorVariant>)message.ColorVariants.Select(cv =>
            cv.Sizes?.Select(
                size =>
                new ColorVariant
                {
                    Color = cv.Color,
                    Quantity = size.Quantity,
                    Size = size.Size
                }
                )
            );
        }
        else
        {
            stock.ColorVariants = message.ColorVariants.Select(cv => new ColorVariant
            {
                Color = cv.Color,
                Quantity = cv.Quantity.Value,
            }).ToList();
        }


        return new CreateStockCommand(stock);
    }
}