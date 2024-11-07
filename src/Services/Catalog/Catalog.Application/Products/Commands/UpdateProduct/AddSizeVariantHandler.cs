using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;

namespace Catalog.Application.Products.Commands.AddSizeVariant;

public record AddSizeVariantCommand(
    Guid Id,
    Guid ColorVariantId,
    string Size,
    decimal Price,
    int Quantity,
    int RestockThreshold) : ICommand<AddSizeVariantResult>;
public record AddSizeVariantResult(bool IsSuccess);

public class AddSizeVariantCommandValidation : AbstractValidator<AddSizeVariantCommand>
{
    public AddSizeVariantCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.ColorVariantId).NotEmpty().WithMessage("ColorVariantId Id is required");
        RuleFor(x => x.Size).NotEmpty().WithMessage("Size Id is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be Greater than 0");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be Greater than 0");
        RuleFor(x => x.RestockThreshold).GreaterThan(0).WithMessage("RestockThreshold must be Greater than 0");
    }
}
public class AddSizeVariantCommandHandler : ICommandHandler<AddSizeVariantCommand, AddSizeVariantResult>
{
    private readonly IProductRepository _productRepository;

    public AddSizeVariantCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<AddSizeVariantResult> Handle(AddSizeVariantCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }
            if(product.ProductType != Domain.Enums.ProductTypeEnum.Clothing)
            {
                throw new NotFoundException($"This product can not have size variants");
            }
            var sizeVariant = SizeVariant.Create(
               ColorVariantId.Of(command.ColorVariantId),
               SizeVariantId.Of(Guid.NewGuid()),
               Size.Of(command.Size),
               Price.Of("$", command.Price),
               Quantity.Of(command.Quantity),
               Quantity.Of(command.RestockThreshold));
            product.AddSizeVariant(ColorVariantId.Of(command.ColorVariantId), sizeVariant);

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new AddSizeVariantResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
