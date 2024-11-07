using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;


namespace Catalog.Application.Products.Commands.AddColorVariantStock;

public record AddColorVariantStockCommand(Guid Id, Guid ColorVariantId, int Quantity) : ICommand<AddColorVariantStockResult>;
public record AddColorVariantStockResult(bool IsSuccess);

public class AddColorVariantStockCommandValidation : AbstractValidator<AddColorVariantStockCommand>
{
    public AddColorVariantStockCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.ColorVariantId).NotEmpty().WithMessage("ColorVariant Id is required");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity Id is required");
    }
}
public class AddColorVariantStockCommandHandler : ICommandHandler<AddColorVariantStockCommand, AddColorVariantStockResult>
{
    private readonly IProductRepository _productRepository;

    public AddColorVariantStockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<AddColorVariantStockResult> Handle(AddColorVariantStockCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }
            product.AddColorVariantStock( ColorVariantId.Of(command.ColorVariantId), ColorVariantQuantity.Of(command.Quantity));
            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new AddColorVariantStockResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
