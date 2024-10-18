using Catalog.Application.Dtos;

namespace Catalog.Application.Extensions;

public static class ProductExtensions
{
    public static IEnumerable<ProductDto> ToProductDtoList(this IEnumerable<Product>  products)
    {
        return products.Select(product => DtoFromProduct(product)).ToList();
    }

    public static ProductDto ToOrderDto(this Product product )
    {
        return DtoFromProduct(product);
    }

    private static ProductDto DtoFromProduct(Product product)
    {
        return new ProductDto(
            Id: product.Id.Value,
            Name: product.Name.Value,
            UrlFriendlyName: product.UrlFriendlyName.Value,
            Description: product.Description.Value,
            IsHandmade: product.IsHandmade,
            CoverImage: new ImageDto(product.CoverImage.ImageSrc, product.CoverImage.AltText),
            ProductTypeId: product.ProductTypeId.Value,
            MaterialId: product.MaterialId.Value,
            CollectionId: product.CollectionId.Value,
            OccasionIds: product.OccasionIds.Select(i => i.Value).ToList(),
            CategoryIds: product.CategoryIds.Select(i => i.Value).ToList(),
            ColorVariants: product.ColorVariants.Select(cv => new OutputColorVariantDto(
                Color: cv.Color.Value,
                Images: cv.Images.Select(im => new ImageDto(im.ImageSrc, im.AltText)).ToList(),
                Price: new PriceDto(cv.Price.Currency, cv.Price.Amount),
                Quantity: cv.Quantity.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new SizeVariantDto(sv.Size.Value, sv.Price.Amount, sv.Price.Currency, sv.Quantity.Value)).ToList()
                )).ToList(),
            ProductType: string.Empty,
            Material: string.Empty,
            Collection: string.Empty,
            Occasions: new(),
            Categories: new()
        );
    }
}
