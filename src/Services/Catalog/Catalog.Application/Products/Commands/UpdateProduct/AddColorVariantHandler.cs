using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;

namespace Catalog.Application.Products.Commands.AddColorVariant;

public record AddColorVariantCommand(
    Guid Id,
    string Color,
    List<ImageDto> Images,
    decimal? Price,
    int? Quantity,
    List<SizeVariantDto>? sizeVariants,
    int? RestockThreshold
) : ICommand<AddColorVariantResult>;
public record AddColorVariantResult(bool IsSuccess);

public class AddColorVariantCommandValidation : AbstractValidator<AddColorVariantCommand>
{
    public AddColorVariantCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
    }
}
public class AddColorVariantCommandHandler : ICommandHandler<AddColorVariantCommand, AddColorVariantResult>
{
    private readonly IProductRepository _productRepository;

    public AddColorVariantCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<AddColorVariantResult> Handle(AddColorVariantCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }

            var newColorVariant = ColorVariant.Create(
                    product.Id,
                    Color.Of(command.Color),
                    Slug.Of(product.UrlFriendlyName.Value, command.Color),
                    ColorVariantPrice.Of(command.Price.HasValue ? "USD" : null, command.Price),
                    ColorVariantQuantity.Of(command.Quantity),
                    ColorVariantQuantity.Of(command.RestockThreshold));

            foreach (var image in command.Images)
            {
                var newImage = Image.Of(image.ImageSrc, image.AltText);
                newColorVariant.AddImage(newImage);
            }

            foreach (var sizeVariant in command.sizeVariants)
            {
                var newSizeVariant = SizeVariant.Create(
                    newColorVariant.Id,
                    SizeVariantId.Of(Guid.NewGuid()),
                    Size.Of(sizeVariant.Size),
                    Price.Of("USD", sizeVariant.Price),
                    Quantity.Of(sizeVariant.Quantity),
                    Quantity.Of(sizeVariant.RestockThreshold));
                newColorVariant.AddSizeVariant(newSizeVariant);
            }
            product.AddColorVariant(newColorVariant);

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new AddColorVariantResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
