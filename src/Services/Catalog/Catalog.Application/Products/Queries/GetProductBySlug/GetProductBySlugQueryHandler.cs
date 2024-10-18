//using BuildingBlocks.CQRS;
//using Catalog.Application.Interfaces;
//using Catalog.Application.Products.Dtos;

//namespace Catalog.Application.Products.Queries.GetProductBySlug;

//public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, GetProductBySlugQueryResult>
//{
//    private readonly IProductRepository _productRepository;
//    private readonly IProductTypeRepository _productTypeRepository;
//    private readonly IMaterialRepository  _materialRepository;
//    private readonly ICollectionRepository  _collectionRepository;

//    public GetProductBySlugQueryHandler(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IMaterialRepository materialRepository, ICollectionRepository collectionRepository)
//    {
//        _productRepository = productRepository;
//        _productTypeRepository = productTypeRepository;
//        _materialRepository = materialRepository;
//        _collectionRepository = collectionRepository;
//    }

//    public async Task<GetProductBySlugQueryResult> Handle(GetProductBySlugQuery query, CancellationToken cancellationToken)
//    {
//        // Récupérer le produit par son Slug
//        var product = await _productRepository.GetBySlugAsync(query.Slug);

//        if (product == null)
//        {
//            throw new KeyNotFoundException($"Product with slug '{query.Slug}' not found.");
//        }
//        var productType = await _productTypeRepository.GetByIdAsync(product.ProductTypeId);
//        var material = await _materialRepository.GetByIdAsync(product.MaterialId);
//        var collection = await _collectionRepository.GetByIdAsync(product.CollectionId);
//        // Mapper l'entité Product vers un DTO
//        var productDto = new ProductDto(
//            product.Id.Value,
//            product.Name.Value,
//            product.Description.Value,
//            product.IsHandmade,
//            productType.Name,
//            material.Name,
//            collection.Name,
//            product.Categories.Select(c => new CategoryDto(c.Id.Value, c.Name.Value)).ToList(),
//            product.Occasions.Select(o => new OccasionDto(o.Id.Value, o.Name.Value)).ToList(),
//            product.ColorVariants.Select(cv => new ColorVariantDto(
//                cv.Id.Value,
//                cv.Color.ToString(),
//                cv.Slug.Value,
//                cv.Images.Select(i => i.ImageSrc).ToList(),
//                cv.SizeVariants != null ?
//                    cv.SizeVariants.Select(sv => new SizeVariantDto(sv.Size.Value, sv.Price.Amount, sv.Quantity.Value)).ToList()
//                    : null,
//                cv.SizeVariants != null ? cv.Price.Amount : (decimal?)null,
//                cv.SizeVariants != null ? cv.Quantity.Value : (int?)null
//            )).ToList()
//        );

//        return new GetProductBySlugQueryResult(productDto);
//    }
//}
