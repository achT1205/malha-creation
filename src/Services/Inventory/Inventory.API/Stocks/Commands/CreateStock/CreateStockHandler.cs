﻿using BuildingBlocks.Enums;

namespace Inventory.API.Stocks.Commands.CreateStock;

public record CreateStockCommand(StockDto Stock) : ICommand<CreateStockResuslt>;
public record CreateStockResuslt(Guid Id);

public class CreateStockCommandValidation : AbstractValidator<CreateStockCommand>
{
    public CreateStockCommandValidation()
    {
        RuleFor(x => x.Stock.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleForEach(x => x.Stock.ColorVariants).ChildRules(cv => cv.RuleFor(x => x.LowStockThreshold).NotEmpty().WithMessage("The LowStockThreshold is required."));
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
public class CreateStockCommandHandler(IDocumentSession session) : ICommandHandler<CreateStockCommand, CreateStockResuslt>
{
    public async Task<CreateStockResuslt> Handle(CreateStockCommand command, CancellationToken cancellationToken)
    {
        var exists = await session.Query<Stock>()
            .Where(_ => _.ProductId == command.Stock.ProductId).FirstOrDefaultAsync();
        if (exists != null)
        {
            throw new StockAlreadyExistsFoundException($"There is already a stock with the same productId {command.Stock.ProductId}");
        }

        var stock = new Stock
        {
            ProductId = command.Stock.ProductId,
            ProductType = command.Stock.ProductType,
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