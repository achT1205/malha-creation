using BuildingBlocks.Enums;

namespace Inventory.API.Stocks.Commands.UpdateStock;

public record UpdateStockCommand(Stock Stock) : ICommand<UpdateStockResuslt>;
public record UpdateStockResuslt(bool IsSuccess);

public class UpdateStockCommandValidation : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockCommandValidation()
    {
        RuleFor(x => x.Stock.Id).NotEmpty().WithMessage("Stock Id is required.");
        RuleFor(x => x.Stock.ProductType).NotEmpty().WithMessage("ProductType is required.");
        When(x => x.Stock.ProductType == ProductType.Clothing.ToString(), () =>
        {
            RuleForEach(x => x.Stock.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
            RuleForEach(x => x.Stock.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Quantity).NotEmpty().WithMessage("The Quantity name is required."));
            RuleForEach(x => x.Stock.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Size).NotEmpty().WithMessage("The Sise name is required."));
        });
        When(x => x.Stock.ProductType == ProductType.Accessory.ToString(), () =>
        {
            RuleForEach(x => x.Stock.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
            RuleForEach(x => x.Stock.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Quantity).NotEmpty().WithMessage("The Quantity name is required."));
        });
    }
}
public class UpdateStockCommandHandler(IDocumentSession session) : ICommandHandler<UpdateStockCommand, UpdateStockResuslt>
{
    public async Task<UpdateStockResuslt> Handle(UpdateStockCommand command, CancellationToken cancellationToken)
    {

        var stock = await session.LoadAsync<Stock>(command.Stock.Id, cancellationToken);
        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ID {command.Stock.Id} is not found");
        }

        stock.ColorVariants = command.Stock.ColorVariants.Select(cv =>
            new ColorVariant
            {
                Color = cv.Color,
                Quantity = cv.Quantity,
                LowStockThreshold = cv.LowStockThreshold,
                Size = cv.Size,

            }).ToList();

        session.Store(stock);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateStockResuslt(true);
    }
}