using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;


namespace Catalog.Application.Products.Commands.UpdateColorVariantPrice;

public record UpdateColorVariantPriceCommand(Guid Id, Guid ColorVariantId, decimal Price) : ICommand<UpdateColorVariantPriceResult>;
public record UpdateColorVariantPriceResult(bool IsSuccess);

public class UpdateColorVariantPriceCommandValidation : AbstractValidator<UpdateColorVariantPriceCommand>
{
    public UpdateColorVariantPriceCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        RuleFor(x => x.ColorVariantId).NotEmpty().WithMessage("ColorVariantId is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}
public class UpdateColorVariantPriceCommandHandler : ICommandHandler<UpdateColorVariantPriceCommand, UpdateColorVariantPriceResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateColorVariantPriceCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<UpdateColorVariantPriceResult> Handle(UpdateColorVariantPriceCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }
            product.UpdateColorVariantPrice(
                ColorVariantId.Of(command.ColorVariantId), 
                ColorVariantPrice.Of("$", command.Price)
                );
            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new UpdateColorVariantPriceResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
