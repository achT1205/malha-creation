using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Application.Products.Dtos;

namespace Catalog.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();

        // Mapper les entités Product vers des DTOs
        var productDtos = products.Select(p => new ProductDto(
            p.Id.Value,
            p.Name,
            p.Description,
            p.IsHandmade,
            p.ProductType.Name,
            p.Material.Name,
            p.Collection.Name,
            p.Categories.Select(c => new CategoryDto(c.Id.Value, c.Name)).ToList(),
            p.Occasions.Select(o => new OccasionDto(o.Id.Value, o.Name)).ToList(),
            p.ColorVariants.Select(cv => new ColorVariantDto(
                cv.Id.Value,
                cv.Color.ToString(),
                cv.Slug.Value,
                cv.Images.Select(i => i.ImageSrc).ToList(),
                cv is ClothingColorVariant clothingVariant ?
                    clothingVariant.SizeVariants.Select(sv => new SizeVariantDto(sv.Size.Value, sv.Price.Value, sv.Quantity.Value)).ToList()
                    : null,
                cv is AccessoryColorVariant accessoryVariant ? accessoryVariant.Price.Value : (decimal?)null,
                cv is AccessoryColorVariant accessoryVariantQuantity ? accessoryVariantQuantity.Quantity.Value : (int?)null
            )).ToList()
        )).ToList();

        return new GetProductsQueryResult(productDtos);
    }
}
