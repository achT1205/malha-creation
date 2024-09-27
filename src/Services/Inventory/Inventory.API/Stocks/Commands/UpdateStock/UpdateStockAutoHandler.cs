using BuildingBlocks.Enums;

namespace Inventory.API.Stocks.Commands.UpdateStock;

public record UpdateStockAutoCommand(Stock Stock) : ICommand<UpdateStockAutoResuslt>;
public record UpdateStockAutoResuslt(bool IsSuccess);

public class UpdateStockAutoCommandValidation : AbstractValidator<UpdateStockAutoCommand>
{
    public UpdateStockAutoCommandValidation()
    {
        RuleFor(x => x.Stock.ProductId).NotEmpty().WithMessage("ProductId Id is required.");
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
public class UpdateStockAutoCommandHandler(IDocumentSession session) : ICommandHandler<UpdateStockAutoCommand, UpdateStockAutoResuslt>
{
    public async Task<UpdateStockAutoResuslt> Handle(UpdateStockAutoCommand command, CancellationToken cancellationToken)
    {

        var stock = await session.Query<Stock>()
           .Where(_ => _.ProductId == command.Stock.ProductId).FirstOrDefaultAsync();
        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ProductId {command.Stock.ProductId} is not found");
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

        return new UpdateStockAutoResuslt(true);
    }
}