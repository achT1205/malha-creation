using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Commands.AddProductVariant;

public class AddProductVariantCommandHandler : ICommandHandler<AddProductVariantCommand, AddProductVariantResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IImageRepository _imageRepository;
    public AddProductVariantCommandHandler(IProductRepository productRepository, IImageRepository imageRepository)
    {
        _productRepository = productRepository;
        _imageRepository = imageRepository;
    }
    public async Task<AddProductVariantResult> Handle(AddProductVariantCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(ProductId.Of(command.ProductId));
        var images = await _imageRepository.GetByIdsAsync(command.ColorVariant.ImageIds.Select(ImageId.Of).ToList());
        var id = Guid.Empty;
        if (command.ColorVariant is CreateAccessoryColorVariantDto accessoryVariant)
        {
            var accessoryColorVariant = AccessoryColorVariant.Create(
                Color.Of(accessoryVariant.Color),
                Slug.Create(product.UrlFriendlyName, accessoryVariant.Color),
                images,
                Price.Of(accessoryVariant.Price),
                Quantity.Of(accessoryVariant.Quantity)
            );
            id = accessoryColorVariant.Id.Value;
            product.AddColorVariant(accessoryColorVariant);
        }
        // Vérifier si c'est une variante de vêtement
        else if (command.ColorVariant is CreateClothingColorVariantDto clothingVariant)
        {
            var clothingColorVariant = ClothingColorVariant.Create(
                Color.Of(clothingVariant.Color),
                Slug.Create(product.UrlFriendlyName, clothingVariant.Color),
                images,
                clothingVariant.SizeVariants.Select(sv => SizeVariant.Create(Size.Of(sv.Size), Price.Of(sv.Price), Quantity.Of(sv.Quantity))).ToList()
               );
            id = clothingColorVariant.Id.Value;
            product.AddColorVariant(clothingColorVariant);
        }

        await _productRepository.SaveChangesAsync();

        return new AddProductVariantResult(id);
    }
}
