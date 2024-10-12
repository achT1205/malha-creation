using BuildingBlocks.Enums;

namespace Inventory.API.Stocks.Commands.UpdateStock;

public record UpdateStockAutoCommand : ICommand<UpdateStockAutoResuslt>
{
    public Guid ProductId { get; set; }  // Clé étrangère vers la table des produits
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
}
public record UpdateStockAutoResuslt(bool IsSuccess);

public class UpdateStockAutoCommandValidation : AbstractValidator<UpdateStockAutoCommand>
{
    public UpdateStockAutoCommandValidation()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId Id is required.");
        RuleFor(x => x.ProductType).NotEmpty().WithMessage("ProductType is required.");
        When(x => x.ProductType == ProductType.Clothing.ToString(), () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Quantity).NotEmpty().WithMessage("The Quantity name is required."));
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Size).NotEmpty().WithMessage("The Sise name is required."));
        });
        When(x => x.ProductType == ProductType.Accessory.ToString(), () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Quantity).NotEmpty().WithMessage("The Quantity name is required."));
        });
    }
}
public class UpdateStockAutoCommandHandler(IDocumentSession session) : ICommandHandler<UpdateStockAutoCommand, UpdateStockAutoResuslt>
{
    public async Task<UpdateStockAutoResuslt> Handle(UpdateStockAutoCommand command, CancellationToken cancellationToken)
    {

        var stock = await session.Query<Stock>()
           .Where(_ => _.ProductId == command.ProductId).FirstOrDefaultAsync();
        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ProductId {command.ProductId} is not found");
        }

        stock.ColorVariants = command.ColorVariants.Select(cv =>
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