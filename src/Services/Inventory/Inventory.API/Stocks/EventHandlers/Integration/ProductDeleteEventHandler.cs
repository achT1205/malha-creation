using BuildingBlocks.Messaging.Events;
using Inventory.API.Stocks.Commands.DeleteStockByProductId;
using MassTransit;

namespace Inventory.API.Stocks.EventHandlers.Integration;
public class ProductDeleteEventHandler(ISender sender, ILogger<ProductDeleteEventHandler> logger)
    : IConsumer<ProductDeletedEvent>
{
    public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
    {

        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        try
        {
            var command = new DeleteStockByProductIdCommand(context.Message.ProductId);
            await sender.Send(command);
        }
        catch (Exception ex)
        {
            throw new ProductDeleteEventHandlerException(ex.Message);
        }
    }

   }