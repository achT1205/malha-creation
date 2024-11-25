namespace Ordering.Application.Orders.Queries.GetStockConfirmedOrders;

public record GetStockConfirmedOrdersQuery: IQuery<GetStockConfirmedOrdersResult>;

public record GetStockConfirmedOrdersResult(IEnumerable<OrderDto> orders);
public class GetStockConfirmedOrdersQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetStockConfirmedOrdersQuery, GetStockConfirmedOrdersResult>
{
    public async Task<GetStockConfirmedOrdersResult> Handle(GetStockConfirmedOrdersQuery query, CancellationToken cancellationToken)
    {

        var orders = await dbContext.Orders.Where(_=> _.Status == OrderStatus.StockConfirmed)
                       .ToListAsync(cancellationToken);

        return new GetStockConfirmedOrdersResult(orders.ToOrderDtoList());
    }
}
