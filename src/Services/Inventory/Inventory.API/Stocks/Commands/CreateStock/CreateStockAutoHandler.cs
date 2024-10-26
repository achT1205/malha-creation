using BuildingBlocks.Enums;

namespace Inventory.API.Stocks.Commands.CreateStock;

public record CreateStockAutoCommand : ICommand<CreateStockAutoResuslt>
{
    public Guid ProductId { get; set; }  // Clé étrangère vers la table des produits
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
};
public record CreateStockAutoResuslt(Guid Id);

public class CreateStockAutoCommandValidation : AbstractValidator<CreateStockAutoCommand>
{
    public CreateStockAutoCommandValidation()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.LowStockThreshold).NotEmpty().WithMessage("The LowStockThreshold is required."));
        RuleFor(x => x.ProductType).NotEmpty().WithMessage("ProductType is required.");
        When(x => x.ProductType == ProductTypeEnum.Clothing.ToString(), () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Quantity).NotEmpty().WithMessage("The Quantity name is required."));
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Size).NotEmpty().WithMessage("The Sise name is required."));
        });
        When(x => x.ProductType == ProductTypeEnum.Accessory.ToString(), () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
            RuleForEach(x => x.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.Quantity).NotEmpty().WithMessage("The Quantity name is required."));
        });
    }
}
public class CreateStockAutoCommandHandler(IDocumentSession session) : ICommandHandler<CreateStockAutoCommand, CreateStockAutoResuslt>
{
    public async Task<CreateStockAutoResuslt> Handle(CreateStockAutoCommand command, CancellationToken cancellationToken)
    {
        var stock = new Stock
        {
            ProductId = command.ProductId,
            ProductType = command.ProductType,
            ColorVariants = command.ColorVariants.Select(cv =>
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

        return new CreateStockAutoResuslt(stock.Id);
    }
}