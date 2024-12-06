namespace Catalog.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    string Description,
    string ShippingAndReturns,
    string? Code,
    bool IsHandmade,
    ImageDto CoverImage,
    Guid MaterialId,
    Guid BrandId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<ColorVariantDto> ColorVariants
   )
    : ICommand<UpdateProductResult>
{
    public ProductStatus Status { get; internal set; }
}

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    //public UpdateProductCommandValidator()
    //{
    //    RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    //    RuleFor(x => x.UrlFriendlyName).NotEmpty().WithMessage("UrlFriendlyName is required.");
    //    RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description  is required");
    //    RuleFor(x => x.MaterialId).NotEmpty().WithMessage("Product Material is required");
    //    RuleFor(x => x.BrandId).NotEmpty().WithMessage("Product Brand  is required");
    //    RuleFor(x => x.CollectionId).NotEmpty().WithMessage("Product Collection is required");
    //    RuleFor(x => x.ProductType).IsInEnum().WithMessage("The ProductType is required.");
    //    RuleFor(x => x.ProductType == ProductType.Clothing || x.ProductType == ProductType.Accessory).NotEmpty().WithMessage("The ProductType can only have value betwen accessory and clothing.");
    //    RuleFor(x => x.UrlFriendlyName)
    //        .Matches(@"^[a-zA-Z0-9 \-]*$")
    //        .WithMessage("The field must not contain special characters.");
    //    RuleFor(x => x.ProductTypeId).IsInEnum().WithMessage("The ProductTypeId is required.");
    //    RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("The CategoryIds are required.");
    //    RuleFor(x => x.CoverImage).NotEmpty().WithMessage("The CoverImage is required.");
    //    RuleFor(x => x.ColorVariants.Count()).GreaterThan(0).WithMessage("ColorVariants is required.");
    //    RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
    //    RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Images.Count()).GreaterThan(0).WithMessage("The number of Images must be greater than 0."));
    //    When(x => x.ProductType == ProductType.Clothing, () =>
    //    {
    //        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.sizeVariants).NotNull().WithMessage("The Sizes can not be null."));
    //        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required for clothing products.")));
    //        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.")));
    //    });
    //    When(x => x.ProductType == ProductType.Accessory, () =>
    //    {
    //        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero."));
    //    });
    //}
}
public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{

    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(
        IProductRepository productRepository
        )
    {
        _productRepository = productRepository;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
        if (product == null)
        {
            throw new NotFoundException($"The product {command.Id} was not found");
        }
        try
        {
            product.UpdateStatus(command.Status);
            product.UpdateCode(product.Code);
            product.UpdateNames(ProductName.Of(command.Name), UrlFriendlyName.Of(command.UrlFriendlyName));
            product.UpdateDescription(ProductDescription.Of(command.Description));
            product.UpdateShippingAndReturns(command.ShippingAndReturns);
            product.UpdateMaterial(MaterialId.Of(command.MaterialId));
            product.UpdateCollection(CollectionId.Of(command.CollectionId));
            product.UpdateBrand(BrandId.Of(command.BrandId));
            product.UpdateHandMade(command.IsHandmade);
            product.UpdateCoverImage(Image.Of(command.CoverImage.ImageSrc, command.CoverImage.AltText));
            UpdateOccasions(command, product);
            UpdateCategories(command, product);
            UpdateColorVanriants(command, product);

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new InternalServerException(ex.InnerException.Message);
        }

        return new UpdateProductResult(true);
    }

    private void UpdateOccasions(UpdateProductCommand command, Product product)
    {
        var occasionToBeDeletedIds = product.OccasionIds.Except(command.OccasionIds.Select(id => OccasionId.Of(id)));
        var occasionToBeAddIds = command.OccasionIds.Select(id => OccasionId.Of(id)).Except(product.OccasionIds);

        if (occasionToBeDeletedIds.Any())
        {
            product.RemoveOccasions(occasionToBeDeletedIds.ToList());
        }

        if (occasionToBeAddIds.Any())
        {
            product.AddOccasions(occasionToBeAddIds.ToList());
        }
    }
    private void UpdateCategories(UpdateProductCommand command, Product product)
    {
        var categoryToBeDeletedIds = product.CategoryIds.Except(command.CategoryIds.Select(id => CategoryId.Of(id)));
        var categoryToBeAddIds = command.CategoryIds.Select(id => CategoryId.Of(id)).Except(product.CategoryIds);


        if (categoryToBeDeletedIds.Any())
        {
            product.RemoveCategories(categoryToBeDeletedIds.ToList());
        }

        if (categoryToBeAddIds.Any())
        {
            product.AddCategories(categoryToBeAddIds.ToList());
        }
    }
    private void UpdateColorVanriants(UpdateProductCommand command, Product product)
    {
        RemoveColorVariants(command, product);

        AddColorVariants(command, product);
    }
    private void RemoveColorVariants(UpdateProductCommand command, Product product)
    {
        foreach (var cvId in product.ColorVariants.Select(cv => cv.Id).ToList())
        {
            product.RemoveColorVariant(cvId);
        }
    }
    private void AddColorVariants(UpdateProductCommand command, Product product)
    {
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
                    ColorVariantQuantity.Of(colorVariant.RestockThreshold),
                    colorVariant.OutfitIds.Select(id=> ColorVariantId.Of(id)).ToList());

            foreach (var image in colorVariant.Images)
            {
                var newImage = Image.Of(image.ImageSrc, image.AltText);
                newColorVariant.AddImage(newImage);
            }

            if (product.ProductType == ProductType.Clothing)
            {
                if (!colorVariant.sizeVariants.Any())
                    throw new ArgumentException("sizeVariants are required for Clothing product.", nameof(Quantity));
                foreach (var sizeVariant in colorVariant.sizeVariants)
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
    }
}
