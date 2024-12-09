using MassTransit.Util;

namespace Catalog.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    string Description,
    ProductStatus Status,
    ProductType ProductType,
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
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);


public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
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

                When(product => product.ProductType == ProductType.Clothing, () => // Clothing
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

                When(product => product.ProductType == ProductType.Accessory, () => // Accessory
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
            product.UpdateCode(command.Code);
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

    private void UpdateImages(ColorVariantDto cv, ColorVariant colorVariant)
    {
        var deleteImgs = colorVariant.Images.Select(im => im.ImageSrc).Except(cv.Images.Select(im => im.ImageSrc)).ToList();
        var newImgs = cv.Images.Select(im => im.ImageSrc).Except(colorVariant.Images.Select(im => im.ImageSrc)).ToList();

        if (deleteImgs.Any())
        {
            foreach (var src in deleteImgs)
            {
                colorVariant.RemoveImage(src);
            }
        }

        if (newImgs.Any())
        {
            foreach (var src in newImgs)
            {
                string altText = cv.Images.First(im => im.ImageSrc == src).AltText;
                colorVariant.AddImage(Image.Of(src, altText));
            }
        }
    }

    private void UpdateOutfits(ColorVariantDto cv, ColorVariant colorVariant)
    {
        var outfitdeleteCvIds = colorVariant.Outfits.Except(cv.OutfitIds.Select(id => ColorVariantId.Of(id))).ToList();
        var outfitToBeAddIds = cv.OutfitIds.Select(id => ColorVariantId.Of(id)).Except(colorVariant.Outfits).ToList();

        if (outfitdeleteCvIds.Any())
        {
            foreach (var id in outfitdeleteCvIds)
            {
                colorVariant.RemoveOutfit(id);
            }
        }

        if (outfitToBeAddIds.Any())
        {
            foreach (var id in outfitToBeAddIds)
            {
                colorVariant.AddOutfit(id);
            }
        }
    }
    private void UpdateOccasions(UpdateProductCommand command, Product product)
    {
        var occasiondeleteCvIds = product.OccasionIds.Except(command.OccasionIds.Select(id => OccasionId.Of(id)));
        var occasionToBeAddIds = command.OccasionIds.Select(id => OccasionId.Of(id)).Except(product.OccasionIds);

        if (occasiondeleteCvIds.Any())
        {
            product.RemoveOccasions(occasiondeleteCvIds.ToList());
        }

        if (occasionToBeAddIds.Any())
        {
            product.AddOccasions(occasionToBeAddIds.ToList());
        }
    }
    private void UpdateCategories(UpdateProductCommand command, Product product)
    {
        var categorydeleteCvIds = product.CategoryIds.Except(command.CategoryIds.Select(id => CategoryId.Of(id)));
        var categoryToBeAddIds = command.CategoryIds.Select(id => CategoryId.Of(id)).Except(product.CategoryIds);


        if (categorydeleteCvIds.Any())
        {
            product.RemoveCategories(categorydeleteCvIds.ToList());
        }

        if (categoryToBeAddIds.Any())
        {
            product.AddCategories(categoryToBeAddIds.ToList());
        }
    }
    private void UpdateColorVanriants(UpdateProductCommand command, Product product)
    {
        var newCvs = command.ColorVariants.Where(cv => cv.Id == null).ToList();

        var updateCvs = command.ColorVariants.Where((cv) => cv.Id != null).ToList();

        var oldCvIds = product.ColorVariants.Select(cv => cv.Id).ToList();

        var inComingCvIds = command.ColorVariants.Where(cv => cv.Id != null)
            .Select(cv => ColorVariantId.Of(cv.Id.Value));

        var deleteCvIds = oldCvIds.Except(inComingCvIds).ToList();


        foreach (var cv in updateCvs)
        {
            UpdateColorVariant(command, product, cv);
        }

        foreach (var cvId in deleteCvIds)
        {
            product.RemoveColorVariant(cvId);
        }

        foreach (var cv in newCvs)
        {
            CreateColorVariant(command, product, cv);
        }


    }
    private void UpdateColorVariant(UpdateProductCommand command, Product product, ColorVariantDto? cv)
    {
        var colorVariant = product.ColorVariants.First(_ => _.Id == ColorVariantId.Of(cv.Id.Value));
        colorVariant.Update(
            Color.Of(cv.Color),
            Slug.Of(UrlFriendlyName.Of(command.UrlFriendlyName), Color.Of(cv.Color)),
            ColorVariantPrice.Of(
                product.ProductType == ProductType.Clothing ? null : "USD",
                cv.Price),
            ColorVariantQuantity.Of(cv.Quantity),
            ColorVariantQuantity.Of(cv.RestockThreshold)
            );
        UpdateImages(cv, colorVariant);
        UpdateOutfits(cv, colorVariant);
        if (product.ProductType == ProductType.Clothing)
            UpdateSizeVariants(cv, colorVariant);
    }

    private void UpdateSizeVariants(ColorVariantDto cv, ColorVariant colorVariant)
    {
        var newSvs = cv.SizeVariants.Where(sv => sv.Id == null);

        var oldSvIds = colorVariant.SizeVariants.Select(sv => sv.Id);

        var inComingSvIds = cv.SizeVariants.Where(sv => sv.Id != null).Select(sv => SizeVariantId.Of(sv.Id.Value));

        var deleteSvIds = oldSvIds.Except(inComingSvIds).ToList();

        var updateSvs = cv.SizeVariants.Where((sv) => sv.Id != null);

        if (updateSvs.Any())
        {
            foreach (var sv in updateSvs)
            {
                UpdateSizeVariant(colorVariant, sv);
            }
        }

        if (deleteSvIds.Any())
        {
            foreach (var id in deleteSvIds)
            {
                colorVariant.RemoveSizeVariant(id);
            }
        }
        if (newSvs.Any())
        {
            foreach (var sv in newSvs)
            {
                CreateSizeVariant(colorVariant, sv);
            }
        }

    }

    private void UpdateSizeVariant(ColorVariant colorVariant, SizeVariantDto? sv)
    {
        var sizeVariant = colorVariant.SizeVariants.First(_ => _.Id == SizeVariantId.Of(sv.Id.Value));
        sizeVariant.Update(
            Size.Of(sv.Size),
            Price.Of("USD", sv.Price),
            Quantity.Of(sv.Quantity),
            Quantity.Of(sv.RestockThreshold));
    }
    private void CreateSizeVariant(ColorVariant colorVariant, SizeVariantDto? sv)
    {
        var newSizeVariant = SizeVariant.Create(
            colorVariant.Id,
            Size.Of(sv.Size),
            Price.Of("USD", sv.Price),
            Quantity.Of(sv.Quantity),
            Quantity.Of(sv.RestockThreshold));
        colorVariant.AddSizeVariant(newSizeVariant);
    }
    private void CreateColorVariant(UpdateProductCommand command, Product product, ColorVariantDto colorVariant)
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
}
