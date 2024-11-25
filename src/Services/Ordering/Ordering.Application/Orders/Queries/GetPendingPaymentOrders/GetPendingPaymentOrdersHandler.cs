namespace Ordering.Application.Orders.Queries.GetPendingPaymentOrders;

public record GetPendingPaymentOrdersQuery: IQuery<GetPendingPaymentOrdersResult>;

public record GetPendingPaymentOrdersResult(IEnumerable<OrderDto> orders);
public class GetPendingPaymentOrdersQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPendingPaymentOrdersQuery, GetPendingPaymentOrdersResult>
{
    public async Task<GetPendingPaymentOrdersResult> Handle(GetPendingPaymentOrdersQuery query, CancellationToken cancellationToken)
    {

        var orders = await dbContext.Orders.Where(_=> _.Status == OrderStatus.PaymentPending)
                       .ToListAsync(cancellationToken);

        return new GetPendingPaymentOrdersResult(orders.ToOrderDtoList());
    }
}
