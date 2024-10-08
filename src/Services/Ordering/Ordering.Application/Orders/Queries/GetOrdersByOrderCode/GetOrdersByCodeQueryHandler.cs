namespace Ordering.Application.Orders.Queries.GetOrdersByOrderCode;
public class GetOrdersByCodeQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByOrderCodeQuery, GetOrdersByOrderCodeResult>
{
    public async Task<GetOrdersByOrderCodeResult> Handle(GetOrdersByOrderCodeQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderCode.Value.Contains(query.Code))
                .OrderBy(o => o.OrderCode.Value)
                .ToListAsync(cancellationToken);

        return new GetOrdersByOrderCodeResult(orders);
    }
}