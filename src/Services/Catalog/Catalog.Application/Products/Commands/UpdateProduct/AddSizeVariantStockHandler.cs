using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;


namespace Catalog.Application.Products.Commands.AddSizeVariantStock;

public record AddSizeVariantStockCommand(Guid Id, Guid ColorVariantId, Guid SizeVariantId, int Quantity) : ICommand<AddSizeVariantStockResult>;
public record AddSizeVariantStockResult(bool IsSuccess);

public class AddSizeVariantStockCommandValidation : AbstractValidator<AddSizeVariantStockCommand>
{
    public AddSizeVariantStockCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.ColorVariantId).NotEmpty().WithMessage("ColorVariant Id is required");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity Id is required");
    }
}
public class AddSizeVariantStockCommandHandler : ICommandHandler<AddSizeVariantStockCommand, AddSizeVariantStockResult>
{
    private readonly IProductRepository _productRepository;

    public AddSizeVariantStockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<AddSizeVariantStockResult> Handle(AddSizeVariantStockCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }
            product.AddSizeVariantStock(ColorVariantId.Of(command.ColorVariantId), SizeVariantId.Of(command.SizeVariantId), Quantity.Of(command.Quantity));
            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new AddSizeVariantStockResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
