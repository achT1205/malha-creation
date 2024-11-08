namespace Catalog.Application.Products.Queries.GetProductForStockValidationById;

public record GetProductForStockValidationByIdQuery(Guid Id) : IQuery<GetProductForStockValidationByIdQueryResult>;

public record GetProductForStockValidationByIdQueryResult(ProductStockDto Product);

public class GetProductForStockValidationByIdQueryHandler : IQueryHandler<GetProductForStockValidationByIdQuery, GetProductForStockValidationByIdQueryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly ICollectionRepository _collectionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOccasionRepository _occasionRepository;
    public GetProductForStockValidationByIdQueryHandler(
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

    public async Task<GetProductForStockValidationByIdQueryResult> Handle(GetProductForStockValidationByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(ProductId.Of(query.Id));

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with Id '{query.Id}' not found.");
        }
        var productType = await _productTypeRepository.GetByIdAsync(product.ProductTypeId);
        var material = await _materialRepository.GetByIdAsync(product.MaterialId);
        var collection = await _collectionRepository.GetByIdAsync(product.CollectionId);
        var categories = await _categoryRepository.GetByIdsAsync(product.CategoryIds.ToList());
        var occasions = await _occasionRepository.GetByIdsAsync(product.OccasionIds.ToList());

        var productDto = product.ProductStockDtoFromProduct();

        return new GetProductForStockValidationByIdQueryResult(productDto);
    }
}
