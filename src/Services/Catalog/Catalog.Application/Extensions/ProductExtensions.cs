namespace Catalog.Application.Extensions;

public static class ProductExtensions
{
    public static IEnumerable<ProductDto> ToProductDtoList(this IEnumerable<Product> products)
    {
        return products.Select(product => DtoFromProduct(product, null, null, null, null, null)).ToList();
    }

    public static ProductDto ToProductDto(
        this Product product,
        string material,
        string collection,
        string productType,
        List<string>? occasions,
        List<string>? categories
        )
    {
        return DtoFromProduct(
            product,
            material,
            collection,
            productType,
            occasions,
            categories
            );
    }

    public static ProductStockDto ProductStockDtoFromProduct(this Product product)
    {
        return new ProductStockDto(
            Id: product.Id.Value,
            ProductType: product.ProductType,
            ColorVariants: product.ColorVariants.Select(cv => new StockColorVariantDto(
                Id: cv.Id.Value,
                Color: cv.Color.Value,
                Quantity: cv.Quantity.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new StockSizeVariantDto(Id: sv.Id.Value, sv.Size.Value, sv.Quantity.Value)).ToList()
                )).ToList());
    }

    private static ProductDto DtoFromProduct(
         Product product,
        string? material,
        string? collection,
        string? productType,
        List<string>? occasions,
        List<string>? categories
        )
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
                Id: cv.Id.Value,
                Color: cv.Color.Value,
                Images: cv.Images.Select(im => new ImageDto(im.ImageSrc, im.AltText)).ToList(),
                Price: new PriceDto(cv.Price.Currency, cv.Price.Amount),
                Quantity: cv.Quantity.Value,
                Slug: cv.Slug.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new SizeVariantDto(sv.Size.Value, sv.Price.Amount, sv.Price.Currency, sv.Quantity.Value, sv.RestockThreshold.Value)).ToList()
                )).ToList(),
            ProductType: productType,
            Material: material,
            Collection: collection,
            Occasions: occasions,
            Categories: categories
        );
    }
}
