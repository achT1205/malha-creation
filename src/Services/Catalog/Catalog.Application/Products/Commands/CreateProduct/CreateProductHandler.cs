using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{

    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository)
    {

        _productRepository = productRepository;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

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
            MaterialId.Of(command.MaterialId),
            CollectionId.Of(command.CollectionId),
            AverageRating.Of(0m, 0)
        );

        foreach (var id in command.OccasionIds)
        {
            product.AddCategory(CategoryId.Of(id));
        }

        foreach (var id in command.CategoryIds)
        {
            product.AddOccasion(OccasionId.Of(id));
        }

        foreach (var colorVariant in command.ColorVariants)
        {

            var newColorVariant = ColorVariant.Create(
                product.Id,
                Color.Of(colorVariant.Color),
                Slug.Of(command.UrlFriendlyName, colorVariant.Color),
                Price.Of(Currency.USD, colorVariant.Price),
                Quantity.Of(colorVariant.Quantity));

            foreach (var image in colorVariant.Images)
            {
                var newImage = Image.Of(image.ImageSrc, image.AltText);
                newColorVariant.AddImage(newImage);
            }

            //foreach (var sizeVariant in colorVariant.sizeVariants)
            //{
            //    var newSizeVariant = SizeVariant.Create(
            //        newColorVariant.Id,
            //        SizeVariantId.Of(Guid.NewGuid()),
            //        Size.Of(sizeVariant.Size),
            //        Price.Of(Currency.USD, sizeVariant.Price),
            //    Quantity.Of(colorVariant.Quantity));
            //    newColorVariant.AddSizeVariant(newSizeVariant);
            //}
            product.AddColorVariant(newColorVariant);
        }

        return product;

    }
}
