using BuildingBlocks.Pagination;

namespace Catalog.Application.Products.Queries.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsQueryResult>;
public record GetProductsQueryResult(PaginatedResult<LiteProductDto> Products);

public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    private readonly IProductRepository _productRepository;


    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;

    }
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var (products, totalCount) = await _productRepository.GetAllAsync();

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        return new GetProductsQueryResult(
            new PaginatedResult<LiteProductDto>(
            pageIndex,
            pageSize,
            totalCount,
            products.ToProductDtoList())
            );
    }
}
