namespace Ordering.Application.Orders.Queries.GetGraceTimeOrders;

public record GetGraceTimeOrdersQuery: IQuery<GetGraceTimeOrdersResult>;

public record GetGraceTimeOrdersResult(IEnumerable<OrderDto> orders);
public class GetGraceTimeOrdersQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetGraceTimeOrdersQuery, GetGraceTimeOrdersResult>
{
    public async Task<GetGraceTimeOrdersResult> Handle(GetGraceTimeOrdersQuery query, CancellationToken cancellationToken)
    {

        var orders = await dbContext.Orders.Where(_=> _.GracePeriodEnd >= DateTime.Now && _.Status == Domain.Orders.Enums.OrderStatus.Pending)
                       .ToListAsync(cancellationToken);

        return new GetGraceTimeOrdersResult(orders.ToOrderDtoList());
    }
}
