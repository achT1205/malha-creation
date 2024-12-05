namespace Catalog.Application.Extensions;

public static class ProductExtensions
{
    public static IEnumerable<LiteProductDto> ToProductDtoList(this IEnumerable<Product> products)
    {
        return products.Select(product => DtoFromProduct(product)).ToList();
    }

    public static ProductDto ToProductDto(
        this Product product,
        Material material,
        Collection collection,
        Brand brand,
        List<Occasion>? occasions,
        List<Category>? categories
        )
    {
        return DtoFromProduct(
            product,
            material,
            collection,
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
            ShippingAndReturns: product.ShippingAndReturns,
            Code: product.Code,
            Status: product.Status,
            IsHandmade: product.IsHandmade,
            ProductType: product.ProductType,
            ProductTypeString: product.ProductType.ToString(),
            CoverImage: new ImageDto(product.CoverImage.ImageSrc, product.CoverImage.AltText),
            Material: new MaterialDto(material.Id.Value, material.Name, material.Description),
            Collection: new CollectionDto(
                collection.Id.Value,
                collection.Name,
                collection.Description,
                new ImageDto(collection.CoverImage.ImageSrc, collection.CoverImage.AltText)),
            Brand: new BrandDto(
                brand.Id.Value,
                brand.Name.Value,
                brand.Description,
                brand.WebsiteUrl.Value,
                new ImageDto(brand.Logo.ImageSrc, brand.Logo.AltText)),
            Occasions: occasions?.Select(i => new OccasionDto(i.Id.Value, i.Name.Value, i.Description)).ToList(),
            Categories: categories?.Select(i =>
            new CategoryDto(
                i.Id.Value,
                i.Name.Value,
                collection.Description,
                new ImageDto(i.CoverImage.ImageSrc, i.CoverImage.AltText)
                )).ToList(),
            ColorVariants: product.ColorVariants.Select(cv => new OutputColorVariantDto(
                Id: cv.Id.Value,
                Color: cv.Color.Value,
                Background: $"bg-{cv.Color.Value.ToLower()}-500",
                Images: cv.Images.Select(im => new ImageDto(im.ImageSrc, im.AltText)).ToList(),
                Price:
                product.ProductType == ProductType.Clothing ? null
                : new PriceDto(cv.Price.Currency, cv.Price.Amount),
                Quantity:
                product.ProductType == ProductType.Clothing ? null
                : cv.Quantity.Value,
                RestockThreshold: cv.RestockThreshold.Value,
                Slug: cv.Slug.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new SizeVariantDto(sv.Id.Value, sv.Size.Value, sv.Price.Amount, sv.Price.Currency, sv.Quantity.Value, sv.RestockThreshold.Value)).ToList()
                )).ToList()
        );
    }

    private static LiteProductDto DtoFromProduct(
     Product product
    )
    {
        return new LiteProductDto(
            Id: product.Id.Value,
            Name: product.Name.Value,
            UrlFriendlyName: product.UrlFriendlyName.Value,
            Description: product.Description.Value,
            ShippingAndReturns: product.ShippingAndReturns,
            Code: product.Code,
            Status: product.Status,
            IsHandmade: product.IsHandmade,
            ProductType: product.ProductType,
            ProductTypeString: product.ProductType.ToString(),
            CoverImage: new ImageDto(product.CoverImage.ImageSrc, product.CoverImage.AltText),
            MaterialId: product.MaterialId.Value,
            CollectionId: product.CollectionId.Value,
            BrandId: product.BrandId.Value,
            OccasionIds: product.OccasionIds?.Select(i => i.Value).ToList(),
            CategoryIds: product.CategoryIds?.Select(i => i.Value).ToList(),
            ColorVariants: product.ColorVariants.Select(cv => new OutputColorVariantDto(
                Id: cv.Id.Value,
                Color: cv.Color.Value,
                Background: $"bg-{cv.Color.Value.ToLower()}-500",
                Images: cv.Images.Select(im => new ImageDto(im.ImageSrc, im.AltText)).ToList(),
                Price:
                product.ProductType == ProductType.Clothing ? null
                : new PriceDto(cv.Price.Currency, cv.Price.Amount),
                Quantity:
                product.ProductType == ProductType.Clothing ? null
                : cv.Quantity.Value,
                RestockThreshold: cv.RestockThreshold.Value,
                Slug: cv.Slug.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new SizeVariantDto(sv.Id.Value, sv.Size.Value, sv.Price.Amount, sv.Price.Currency, sv.Quantity.Value, sv.RestockThreshold.Value)).ToList()
                )).ToList()
        );
    }
}
