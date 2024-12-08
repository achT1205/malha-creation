namespace Ordering.Application.Orders.Queries.GetOrdersByOrderCode;
public class GetOrdersByCodeQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByOrderCodeQuery, GetOrderByOrderCodeResult>
{
    public async Task<GetOrderByOrderCodeResult> Handle(GetOrderByOrderCodeQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .FirstAsync(o => o.OrderCode.Value.Contains(query.Code));

        return new GetOrderByOrderCodeResult(order.ToOrderDto());
    }
}