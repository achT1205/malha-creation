//using BuildingBlocks.CQRS;
//using Catalog.Application.Interfaces;
//using Catalog.Application.Products.Dtos;

//namespace Catalog.Application.Products.Queries.GetProducts;

//public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
//{
//    private readonly IProductRepository _productRepository;
//    private readonly IProductTypeRepository _productTypeRepository;
//    private readonly IMaterialRepository _materialRepository;
//    private readonly ICollectionRepository _collectionRepository;

//    public GetProductsQueryHandler(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IMaterialRepository materialRepository, ICollectionRepository collectionRepository)
//    {
//        _productRepository = productRepository;
//        _productTypeRepository = productTypeRepository;
//        _materialRepository = materialRepository;
//        _collectionRepository = collectionRepository;
//    }
//    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
//    {
//        var products = await _productRepository.GetAllAsync();
//        //var productType = await _productTypeRepository.GetByIdAsync(product.ProductTypeId);
//        //var material = await _materialRepository.GetByIdAsync(product.MaterialId);
//        //var collection = await _collectionRepository.GetByIdAsync(product.CollectionId);
//        // Mapper les entités Product vers des DTOs
//        var productDtos = products.Select(p => new ProductDto(
//            p.Id.Value,
//            p.Name.Value,
//            p.Description.Value,
//            p.IsHandmade,
//            "", "", "",
//            //p.ProductType.Name,
//            //p.Material.Name,
//            //p.Collection.Name,
//            p.Categories.Select(c => new CategoryDto(c.Id.Value, c.Name.Value)).ToList(),
//            p.Occasions.Select(o => new OccasionDto(o.Id.Value, o.Name.Value)).ToList(),
//            p.ColorVariants.Select(cv => new ColorVariantDto(
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
//        )).ToList();

//        return new GetProductsQueryResult(productDtos);
//    }
//}
