namespace Ordering.Application.Orders.Queries.GetGraceTimeConfirmedOrders;

public record GetGraceTimeConfirmedOrdersQuery: IQuery<GetGraceTimeConfirmedOrdersResult>;

public record GetGraceTimeConfirmedOrdersResult(IEnumerable<OrderDto> orders);
public class GetGraceTimeConfirmedOrdersQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetGraceTimeConfirmedOrdersQuery, GetGraceTimeConfirmedOrdersResult>
{
    public async Task<GetGraceTimeConfirmedOrdersResult> Handle(GetGraceTimeConfirmedOrdersQuery query, CancellationToken cancellationToken)
    {

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders.Where(_=> _.Status == Domain.Orders.Enums.OrderStatus.GracePeriodConfirmed)
                       .ToListAsync(cancellationToken);


        return new GetGraceTimeConfirmedOrdersResult(orders.ToOrderDtoList());
    }
}
