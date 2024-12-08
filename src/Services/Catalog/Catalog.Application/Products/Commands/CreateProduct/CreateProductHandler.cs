namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string UrlFriendlyName,
    string Description,
    string ShippingAndReturns,
    string? Code,
    bool IsHandmade,
    ImageDto CoverImage,
    Guid ProductTypeId,
    ProductType ProductType,
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
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(1).WithMessage("Name must have at least 1 character.");

        RuleFor(x => x.UrlFriendlyName)
            .NotEmpty().WithMessage("URL-friendly name is required.")
            .MinimumLength(1).WithMessage("URL-friendly name must have at least 1 character.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.");

        RuleFor(x => x.CoverImage)
            .NotNull().WithMessage("Cover image is required.")
            .ChildRules(coverImage =>
            {
                coverImage.RuleFor(ci => ci.ImageSrc)
                    .NotEmpty().WithMessage("Image source is required.")
                    .Must(IsValidUrl).WithMessage("Image source must be a valid URL.");

                coverImage.RuleFor(ci => ci.AltText)
                    .NotEmpty().WithMessage("Alt text is required.");
            });

        RuleFor(x => x.OccasionIds)
            .NotEmpty().WithMessage("At least one occasion is required.");

        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("At least one category is required.");

        RuleFor(x => x.ColorVariants)
            .NotEmpty().WithMessage("At least one color variant is required.")
            .ForEach(variant =>
            {
                variant.ChildRules(colorVariant =>
                {
                    colorVariant.RuleFor(cv => cv.Color)
                        .NotEmpty().WithMessage("Color is required.");

                    colorVariant.RuleForEach(cv => cv.Images)
                        .ChildRules(image =>
                        {
                            image.RuleFor(img => img.ImageSrc)
                                .NotEmpty().WithMessage("Image source is required.")
                                .Must(IsValidUrl).WithMessage("Image source must be a valid URL.");

                            image.RuleFor(img => img.AltText)
                                .NotEmpty().WithMessage("Alt text is required.");
                        });
                });

                When(product => product.ProductType ==  ProductType.Clothing, () => // Clothing
                {
                    variant.ChildRules(colorVariant =>
                    {
                        colorVariant.RuleForEach(cv => cv.SizeVariants)
                            .ChildRules(sizeVariant =>
                            {
                                sizeVariant.RuleFor(sv => sv.Size)
                                    .NotEmpty().WithMessage("Size is required.");

                                sizeVariant.RuleFor(sv => sv.Price)
                                    .GreaterThan(0).WithMessage("Price must be greater than 0.");

                                sizeVariant.RuleFor(sv => sv.Currency)
                                    .NotEmpty().WithMessage("Currency is required.");

                                sizeVariant.RuleFor(sv => sv.Quantity)
                                    .GreaterThanOrEqualTo(0).WithMessage("Quantity must be a non-negative integer.");

                                sizeVariant.RuleFor(sv => sv.RestockThreshold)
                                    .GreaterThanOrEqualTo(0).WithMessage("Restock threshold must be a non-negative integer.");
                            });
                    });
                });

                When(product => product.ProductType ==  ProductType.Accessory, () => // Accessory
                {
                    variant.ChildRules(colorVariant =>
                    {
                        colorVariant.RuleFor(cv => cv.Price)
                            .GreaterThan(0).WithMessage("Price must be greater than 0.");

                        colorVariant.RuleFor(cv => cv.Currency)
                            .NotEmpty().WithMessage("Currency is required.");

                        colorVariant.RuleFor(cv => cv.Quantity)
                            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be a non-negative integer.");

                        colorVariant.RuleFor(cv => cv.RestockThreshold)
                            .GreaterThanOrEqualTo(0).WithMessage("Restock threshold must be a non-negative integer.");
                    });
                });
            });
    }

    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{

    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository
        )
    {
        _productRepository = productRepository;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command);
        try
        {

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
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
            ProductName.Of(command.Name),
            UrlFriendlyName.Of(command.UrlFriendlyName),
            ProductDescription.Of(command.Description),
            command.ShippingAndReturns,
            command.Code,
            command.IsHandmade,
            coverImage,
            command.ProductType,
            MaterialId.Of(command.MaterialId),
            BrandId.Of(command.BrandId),
            CollectionId.Of(command.CollectionId)
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
            if (product.ProductType != ProductType.Clothing)
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
                    Slug.Of(UrlFriendlyName.Of(command.UrlFriendlyName), Color.Of(colorVariant.Color)),
                    ColorVariantPrice.Of(
                        product.ProductType == ProductType.Clothing ? null : "USD",
                        colorVariant.Price),
                    ColorVariantQuantity.Of(colorVariant.Quantity),
                    ColorVariantQuantity.Of(colorVariant.RestockThreshold));

            if (colorVariant.OutfitIds.Any())
            {
                foreach (var id in colorVariant.OutfitIds)
                {
                    newColorVariant.AddOutfit(ColorVariantId.Of(id));
                }
            }

            foreach (var image in colorVariant.Images)
            {
                var newImage = Image.Of(image.ImageSrc, image.AltText);
                newColorVariant.AddImage(newImage);
            }

            if (product.ProductType == ProductType.Clothing)
            {
                if (!colorVariant.SizeVariants.Any())
                    throw new ArgumentException("sizeVariants are required for Clothing product.", nameof(Quantity));
                foreach (var sizeVariant in colorVariant.SizeVariants)
                {
                    var newSizeVariant = SizeVariant.Create(
                        newColorVariant.Id,
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
