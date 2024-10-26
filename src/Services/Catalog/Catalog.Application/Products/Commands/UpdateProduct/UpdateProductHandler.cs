using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            var updatedProduct = UpdatePRoductEntity(product, command);
            _productRepository.UpdateAsync(updatedProduct);
            await _productRepository.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private Product UpdatePRoductEntity(Product product, UpdateProductCommand command)
    {
        product.UpdateNames(ProductName.Of(command.Name), UrlFriendlyName.Of(command.UrlFriendlyName));
        product.UpdateDescription(ProductDescription.Of(command.Description)); 
        product.UpdateMaterial(MaterialId.Of(command.MaterialId));
        product.UpdateCollection(CollectionId.Of(command.CollectionId));
        if(command.RemovedItems != null)
        {
            if (command.RemovedItems.OccasionIds.Any())
            {
                foreach (var occasionId in command.RemovedItems.OccasionIds)
                {
                    product.RemoveOccasion(OccasionId.Of(occasionId));
                }
            }
            if (command.RemovedItems.CategoryIds.Any())
            {
                foreach (var categoryId in command.RemovedItems.CategoryIds)
                {
                    product.RemoveCategory(CategoryId.Of(categoryId));
                }
            }
            if (command.RemovedItems.ColorVariantIds.Any())
            {
                foreach (var colorVariantId in command.RemovedItems.ColorVariantIds)
                {
                    product.RemoveColorVariant(ColorVariantId.Of(colorVariantId));
                }
            }
        }

        foreach (var categoryId in command.CategoryIds)
        {
            product.AddCategory(CategoryId.Of(categoryId));
        }
        foreach (var occasionId in command.OccasionIds)
        {
            product.AddOccasion(OccasionId.Of(occasionId));
        }
        foreach (var colorVariant in command.ColorVariants)
        {
            var newColorVariant = ColorVariant.Create(
                product.Id,
                Color.Of(colorVariant.Color),
                Slug.Of(command.UrlFriendlyName, colorVariant.Color),
                ColorVariantPrice.Of(colorVariant.Price.HasValue ? "USD" : null, colorVariant.Price),
                ColorVariantQuantity.Of(colorVariant.Quantity));

            foreach (var image in colorVariant.Images)
            {
                var newImage = Image.Of(image.ImageSrc, image.AltText);
                newColorVariant.AddImage(newImage);
            }

            foreach (var sizeVariant in colorVariant.sizeVariants)
            {
                var newSizeVariant = SizeVariant.Create(
                    newColorVariant.Id,
                    SizeVariantId.Of(Guid.NewGuid()),
                    Size.Of(sizeVariant.Size),
                    Price.Of("USD", sizeVariant.Price),
                Quantity.Of(sizeVariant.Quantity));
                newColorVariant.AddSizeVariant(newSizeVariant);
            }
            product.AddColorVariant(newColorVariant);
        }

        return product;
    }
}
