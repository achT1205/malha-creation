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
    string Currency,
    int Quantity,
    int RestockThreshold) : ICommand<AddSizeVariantResult>;
public record AddSizeVariantResult(bool IsSuccess);

public class AddSizeVariantCommandValidation : AbstractValidator<AddSizeVariantCommand>
{
    public AddSizeVariantCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
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
            var cv = product.ColorVariants.FirstOrDefault(_ => _.Equals(command.ColorVariantId));
            if (cv == null)
            {
                throw new NotFoundException($"The ColorVariant {command.ColorVariantId} was not found");
            }
            var sizeVariant = SizeVariant.Create(
                ColorVariantId.Of(command.ColorVariantId),
                SizeVariantId.Of(Guid.NewGuid()),
                Size.Of(command.Size),
                Price.Of("$", command.Price),
                Quantity.Of(command.Quantity),
                Quantity.Of(command.RestockThreshold));
            cv.AddSizeVariant(sizeVariant);

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
