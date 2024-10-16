using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Application.Products.Dtos;

namespace Catalog.Application.Products.Queries.GetProductBySlug;

public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, GetProductBySlugQueryResult>
{
    private readonly IProductRepository _productRepository;

    public GetProductBySlugQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductBySlugQueryResult> Handle(GetProductBySlugQuery query, CancellationToken cancellationToken)
    {
        // Récupérer le produit par son Slug
        var product = await _productRepository.GetBySlugAsync(query.Slug);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with slug '{query.Slug}' not found.");
        }

        // Mapper l'entité Product vers un DTO
        var productDto = new ProductDto(
            product.Id.Value,
            product.Name,
            product.Description,
            product.IsHandmade,
            product.ProductType.Name,
            product.Material.Name,
            product.Collection.Name,
            product.Categories.Select(c => new CategoryDto(c.Id.Value, c.Name)).ToList(),
            product.Occasions.Select(o => new OccasionDto(o.Id.Value, o.Name)).ToList(),
            product.ColorVariants.Select(cv => new ColorVariantDto(
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
        );

        return new GetProductBySlugQueryResult(productDto);
    }
}
