
namespace Inventory.API.Stocks.Queries.GetStocks;
public record GetStocksQuery() :IQuery<GetStocksResult>;
public record GetStocksResult(IEnumerable<Stock> Stocks);
public class GetStocksQueryHandler(IDocumentSession session) : IQueryHandler<GetStocksQuery, GetStocksResult>
{
    public async  Task<GetStocksResult>  Handle(GetStocksQuery query, CancellationToken cancellationToken)
    {
        var stocks = await session.Query<Stock>().ToListAsync(cancellationToken);

        return new GetStocksResult(stocks);
    }
}