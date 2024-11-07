using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
using Catalog.Application.Events.IntegrationEvents;
using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Domain.Enums;
using Catalog.Domain.ValueObjects;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    ImageDto CoverImage,
    Guid ProductTypeId,
    ProductTypeEnum ProductType,
    Guid MaterialId,
    Guid BrandId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<ColorVariantDto> ColorVariants
   )
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.UrlFriendlyName).NotEmpty().WithMessage("UrlFriendlyName is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description  is required");
        RuleFor(x => x.MaterialId).NotEmpty().WithMessage("Product Material is required");
        RuleFor(x => x.BrandId).NotEmpty().WithMessage("Product Brand  is required");
        RuleFor(x => x.CollectionId).NotEmpty().WithMessage("Product Collection is required");
        RuleFor(x => x.ProductType).IsInEnum().WithMessage("The ProductType is required.");
        RuleFor(x => x.ProductType == ProductTypeEnum.Clothing || x.ProductType == ProductTypeEnum.Accessory).NotEmpty().WithMessage("The ProductType can only have value betwen accessory and clothing.");
        RuleFor(x => x.UrlFriendlyName)
            .Matches(@"^[a-zA-Z0-9 \-]*$")
            .WithMessage("The field must not contain special characters.");
        RuleFor(x => x.ProductTypeId).IsInEnum().WithMessage("The ProductTypeId is required.");
        RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("The CategoryIds are required.");
        RuleFor(x => x.CoverImage).NotEmpty().WithMessage("The CoverImage is required.");
        RuleFor(x => x.ColorVariants.Count()).GreaterThan(0).WithMessage("ColorVariants is required.");
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Images.Count()).GreaterThan(0).WithMessage("The number of Images must be greater than 0."));
        When(x => x.ProductType == ProductTypeEnum.Clothing, () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.sizeVariants).NotNull().WithMessage("The Sizes can not be null."));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required for clothing products.")));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.")));
        });
        When(x => x.ProductType == ProductTypeEnum.Accessory, () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero."));
        });
    }
}
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{

    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IPublishEndpoint publishEndpoint
        )
    {
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command);
        try
        {

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();


            var dto = product.ToProductDto(null, null, null, null, null);
            var eventMessage = dto.Adapt<ProductCreatedEvent>();
            await _publishEndpoint.Publish(eventMessage, cancellationToken);

        }
        catch (Exception ex)
        {
            throw new InternalServerException(ex.InnerException.Message);
        }

        return new CreateProductResult(product.Id.Value);
    }

    private Product CreateNewProduct(CreateProductCommand command)
    {
        var coverImage = Image.Of(command.CoverImage.ImageSrc, command.CoverImage.AltText);
        var product = Product.Create(
            ProductId.Of(Guid.NewGuid()),
            ProductName.Of(command.Name),
            UrlFriendlyName.Of(command.UrlFriendlyName),
            ProductDescription.Of(command.Description),
            command.IsHandmade,
            coverImage,
            ProductTypeId.Of(command.ProductTypeId),
            command.ProductType,
            MaterialId.Of(command.MaterialId),
            BrandId.Of(command.BrandId),
            CollectionId.Of(command.CollectionId),
            AverageRating.Of(0m, 0)
        );

        foreach (var id in command.CategoryIds)
        {
            product.AddCategory(CategoryId.Of(id));
        }

        foreach (var id in command.OccasionIds)
        {
            product.AddOccasion(OccasionId.Of(id));
        }
        foreach (var colorVariant in command.ColorVariants)
        {
            if (product.ProductType != Domain.Enums.ProductTypeEnum.Clothing)
            {
                if (!colorVariant.Price.HasValue || colorVariant.Price.Value <= 0)
                    throw new ArgumentException("Price must greater than 0.", nameof(Price));
                if (!colorVariant.Quantity.HasValue || colorVariant.Quantity.Value <= 0)
                    throw new ArgumentException("Quantity must greater than 0.", nameof(Quantity));
                if (!colorVariant.RestockThreshold.HasValue || colorVariant.RestockThreshold.Value <= 0)
                    throw new ArgumentException("RestockThreshold must greater than 0.", nameof(Quantity));
            }

            var newColorVariant = ColorVariant.Create(
                    product.Id,
                    Color.Of(colorVariant.Color),
                    Slug.Of(command.UrlFriendlyName, colorVariant.Color),
                    ColorVariantPrice.Of("USD", colorVariant.Price),
                    ColorVariantQuantity.Of(colorVariant.Quantity),
                    ColorVariantQuantity.Of(colorVariant.RestockThreshold));

            foreach (var image in colorVariant.Images)
            {
                var newImage = Image.Of(image.ImageSrc, image.AltText);
                newColorVariant.AddImage(newImage);
            }

            if (product.ProductType == Domain.Enums.ProductTypeEnum.Clothing)
            {
                if (!colorVariant.sizeVariants.Any())
                    throw new ArgumentException("sizeVariants are required for Clothing product.", nameof(Quantity));
                foreach (var sizeVariant in colorVariant.sizeVariants)
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
            }
            product.AddColorVariant(newColorVariant);
        }

        return product;

    }
}
