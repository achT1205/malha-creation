namespace Inventory.API.Stocks.Commands.CreateStock;

public record CreateStockCommand(Stock Stock) : ICommand<CreateStockResuslt>;
public record CreateStockResuslt(Guid Id);
public class CreateStockCommandHandler(IDocumentSession session) : ICommandHandler<CreateStockCommand, CreateStockResuslt>
{
    public async Task<CreateStockResuslt> Handle(CreateStockCommand command, CancellationToken cancellationToken)
    {
        var stock = new Stock
        {
            ProductId = command.Stock.ProductId,
            CreatedAt = command.Stock.CreatedAt,
            ColorVariants = command.Stock.ColorVariants.Select(cv =>
            new ColorVariant
            {
                Color = cv.Color,
                Quantity = cv.Quantity,
                LowStockThreshold = cv.LowStockThreshold,
                Size = cv.Size,

            }).ToList(),
        };
        session.Store(stock);

        await session.SaveChangesAsync(cancellationToken);

        return new CreateStockResuslt(stock.Id);
    }
}