using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;

namespace Catalog.Application.Products.Commands.RemoveSizeVariant;

public record RemoveSizeVariantCommand(Guid Id, Guid ColorVariantId, Guid SizeVariantId) : ICommand<RemoveSizeVariantResult>;
public record RemoveSizeVariantResult(bool IsSuccess);

public class RemoveSizeVariantCommandValidation : AbstractValidator<RemoveSizeVariantCommand>
{
    public RemoveSizeVariantCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
    }
}
public class RemoveSizeVariantCommandHandler : ICommandHandler<RemoveSizeVariantCommand, RemoveSizeVariantResult>
{
    private readonly IProductRepository _productRepository;

    public RemoveSizeVariantCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<RemoveSizeVariantResult> Handle(RemoveSizeVariantCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }
            product.RemoveSizeVariant(ColorVariantId.Of(command.ColorVariantId), SizeVariantId.Of(command.SizeVariantId));

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new RemoveSizeVariantResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
