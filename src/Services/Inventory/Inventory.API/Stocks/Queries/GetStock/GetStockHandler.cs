
namespace Inventory.API.Stocks.Queries.GetStock;
public record GetStockQuery(Guid Id) : IQuery<GetStockResult>;
public record GetStockResult(Stock Stock);
public class GetStockQueryHandler(IDocumentSession session) : IQueryHandler<GetStockQuery, GetStockResult>
{
    public async Task<GetStockResult> Handle(GetStockQuery query, CancellationToken cancellationToken)
    {
        var stock = await session.LoadAsync<Stock>(query.Id, cancellationToken);

        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ProductId {query.Id} is not found");
        }

        return new GetStockResult(stock);
    }
}