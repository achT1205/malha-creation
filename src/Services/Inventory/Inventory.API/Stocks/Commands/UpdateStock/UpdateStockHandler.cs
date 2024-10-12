using BuildingBlocks.Enums;

namespace Inventory.API.Stocks.Commands.UpdateStock;

public record UpdateStockCommand : ICommand<UpdateStockResuslt>
{
    public Guid Id { get; set; } = Guid.NewGuid();  // Identifiant unique pour l'entrée de stock
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs

}
public record UpdateStockResuslt(bool IsSuccess);

public class UpdateStockCommandValidation : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Stock Id is required.");
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
public class UpdateStockCommandHandler(IDocumentSession session) : ICommandHandler<UpdateStockCommand, UpdateStockResuslt>
{
    public async Task<UpdateStockResuslt> Handle(UpdateStockCommand command, CancellationToken cancellationToken)
    {

        var stock = await session.LoadAsync<Stock>(command.Id, cancellationToken);
        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ID {command.Id} is not found");
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

        return new UpdateStockResuslt(true);
    }
}