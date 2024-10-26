using BuildingBlocks.CQRS;
using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;

namespace Catalog.Application.Products.Queries.GetProductBySlug;

public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, GetProductBySlugQueryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly ICollectionRepository _collectionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOccasionRepository _occasionRepository;
    public GetProductBySlugQueryHandler(
        IProductRepository productRepository,
        IProductTypeRepository productTypeRepository,
        IMaterialRepository materialRepository,
        ICollectionRepository collectionRepository,
        ICategoryRepository categoryRepository,
        IOccasionRepository occasionRepository)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _materialRepository = materialRepository;
        _collectionRepository = collectionRepository;
        _categoryRepository = categoryRepository;
        _occasionRepository = occasionRepository;
    }

    public async Task<GetProductBySlugQueryResult> Handle(GetProductBySlugQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetBySlugAsync(query.Slug);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with slug '{query.Slug}' not found.");
        }
        var productType = await _productTypeRepository.GetByIdAsync(product.ProductTypeId);
        var material = await _materialRepository.GetByIdAsync(product.MaterialId);
        var collection = await _collectionRepository.GetByIdAsync(product.CollectionId);
        var categories = await _categoryRepository.GetByIdsAsync(product.CategoryIds.ToList());
        var occasions = await _occasionRepository.GetByIdsAsync(product.OccasionIds.ToList());

        var productDto = product.ToOrderDto(
            material.Name,
            collection.Name,
            productType.Name,
            occasions.Select(o => o.Name.Value).ToList(),
            categories.Select(c => c.Name.Value).ToList());

        return new GetProductBySlugQueryResult(productDto);
    }
}
