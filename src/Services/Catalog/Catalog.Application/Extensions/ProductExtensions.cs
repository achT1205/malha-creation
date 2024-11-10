namespace Catalog.Application.Extensions;

public static class ProductExtensions
{
    public static IEnumerable<ProductDto> ToProductDtoList(this IEnumerable<Product> products)
    {
        return products.Select(product => DtoFromProduct(product)).ToList();
    }

    public static ProductDto ToProductDto(
        this Product product,
        Material material,
        Collection collection,
        ProductType productType,
        Brand brand,
        List<Occasion>? occasions,
        List<Category>? categories
        )
    {
        return DtoFromProduct(
            product,
            material,
            collection,
            productType,
            brand,
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
        Material material,
        Collection collection,
        ProductType productType,
        Brand brand,
        List<Occasion>? occasions,
        List<Category>? categories
        )
    {
        return new ProductDto(
            Id: product.Id.Value,
            Name: product.Name.Value,
            UrlFriendlyName: product.UrlFriendlyName.Value,
            Description: product.Description.Value,
            IsHandmade: product.IsHandmade,
            CoverImage: new ImageDto(product.CoverImage.ImageSrc, product.CoverImage.AltText),
            ProductType: new ProductTypeDto(productType.Id.Value, productType.Name),
            Material: new MaterialDto(material.Id.Value, material.Name),
            Collection: new CollectionDto(collection.Id.Value, collection.Name, collection.Image.ImageSrc, collection.Image.AltText),
            Brand: new BrandDto(brand.Id.Value, brand.Name.Value),
            Occasions: occasions?.Select(i => new OccasionDto(i.Id.Value, i.Name.Value)).ToList(),
            Categories: categories?.Select(i => new CategoryDto(i.Id.Value, i.Name.Value)).ToList(),
            ColorVariants: product.ColorVariants.Select(cv => new OutputColorVariantDto(
                Id: cv.Id.Value,
                Color: cv.Color.Value,
                Images: cv.Images.Select(im => new ImageDto(im.ImageSrc, im.AltText)).ToList(),
                Price: new PriceDto(cv.Price.Currency, cv.Price.Amount),
                Quantity: cv.Quantity.Value,
                RestockThreshold: cv.RestockThreshold.Value,
                Slug: cv.Slug.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new SizeVariantDto(sv.Id.Value, sv.Size.Value, sv.Price.Amount, sv.Price.Currency, sv.Quantity.Value, sv.RestockThreshold.Value)).ToList()
                )).ToList()
        );
    }

    private static ProductDto DtoFromProduct(
     Product product
    )
    {
        return new ProductDto(
            Id: product.Id.Value,
            Name: product.Name.Value,
            UrlFriendlyName: product.UrlFriendlyName.Value,
            Description: product.Description.Value,
            IsHandmade: product.IsHandmade,
            CoverImage: new ImageDto(product.CoverImage.ImageSrc, product.CoverImage.AltText),
            ProductType: new ProductTypeDto(product.ProductTypeId.Value, ""),
            Material: new MaterialDto(product.MaterialId.Value, ""),
            Collection: new CollectionDto(product.CollectionId.Value,"","", ""),
            Brand: new BrandDto(product.BrandId.Value, ""),
            Occasions: product.OccasionIds?.Select(i => new OccasionDto(i.Value, "")).ToList(),
            Categories: product.CategoryIds?.Select(i => new CategoryDto(i.Value,"")).ToList(),
            ColorVariants: product.ColorVariants.Select(cv => new OutputColorVariantDto(
                Id: cv.Id.Value,
                Color: cv.Color.Value,
                Images: cv.Images.Select(im => new ImageDto(im.ImageSrc, im.AltText)).ToList(),
                Price: new PriceDto(cv.Price.Currency, cv.Price.Amount),
                Quantity: cv.Quantity.Value,
                RestockThreshold: cv.RestockThreshold.Value,
                Slug: cv.Slug.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new SizeVariantDto(sv.Id.Value, sv.Size.Value, sv.Price.Amount, sv.Price.Currency, sv.Quantity.Value, sv.RestockThreshold.Value)).ToList()
                )).ToList()
        );
    }
}
