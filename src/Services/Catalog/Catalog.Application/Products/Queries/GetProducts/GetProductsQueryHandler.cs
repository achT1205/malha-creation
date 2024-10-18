using BuildingBlocks.CQRS;
using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;

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
        

        return new GetProductsQueryResult(products.ToProductDtoList());
    }
}
