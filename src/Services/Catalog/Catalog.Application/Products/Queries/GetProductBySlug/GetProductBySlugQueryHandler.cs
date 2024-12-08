namespace Catalog.Application.Products.Queries.GetProductBySlug;

public record GetProductBySlugQuery(string Slug) : IQuery<GetProductByIdQueryResult>;
public record GetProductByIdQueryResult(ProductDto Product);
public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, GetProductByIdQueryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly ICollectionRepository _collectionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOccasionRepository _occasionRepository;
    private readonly IBrandRepository _brandRepository;
    public GetProductBySlugQueryHandler(
        IProductRepository productRepository,
        IMaterialRepository materialRepository,
        ICollectionRepository collectionRepository,
        ICategoryRepository categoryRepository,
        IOccasionRepository occasionRepository,
        IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _materialRepository = materialRepository;
        _collectionRepository = collectionRepository;
        _categoryRepository = categoryRepository;
        _occasionRepository = occasionRepository;
        _brandRepository = brandRepository;
    }

    public async Task<GetProductByIdQueryResult> Handle(GetProductBySlugQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetBySlugAsync(query.Slug);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with slug '{query.Slug}' not found.");
        }
        var material = await _materialRepository.GetByIdAsync(product.MaterialId);
        var brand = await _brandRepository.GetByIdAsync(product.BrandId);
        var collection = await _collectionRepository.GetByIdAsync(product.CollectionId);
        var categories = await _categoryRepository.GetByIdsAsync(product.CategoryIds.ToList());
        var occasions = await _occasionRepository.GetByIdsAsync(product.OccasionIds.ToList());

        var productDto = product.ToProductDto(
            material,
            collection,
            brand,
            occasions,
            categories);

        return new GetProductByIdQueryResult(productDto);
    }
}
