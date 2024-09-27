
namespace Inventory.API.Stocks.Queries.GetStockByProductId;
public record GetStockByProductIdQuery(Guid Id) : IQuery<GetStockByProductIdResult>;
public record GetStockByProductIdResult(Stock Stock);
public class GetStockByProductIdQueryHandler(IDocumentSession session) : IQueryHandler<GetStockByProductIdQuery, GetStockByProductIdResult>
{
    public async Task<GetStockByProductIdResult> Handle(GetStockByProductIdQuery query, CancellationToken cancellationToken)
    {
        var stock = await session.Query<Stock>()
            .Where(_ => _.ProductId == query.Id).FirstOrDefaultAsync();

        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ProductId {query.Id} is not found");
        }

        return new GetStockByProductIdResult(stock);
    }
}