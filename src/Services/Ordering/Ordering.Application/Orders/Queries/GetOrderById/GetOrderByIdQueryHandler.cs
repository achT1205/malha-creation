namespace Ordering.Application.Orders.Queries.GetOrdersById;

public record GetOrdersByIdQuery(Guid Id)
    : IQuery<GetOrderByIdResult>;
public record GetOrderByIdResult(OrderDto Order);


public class GetOrderByIdQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByIdQuery, GetOrderByIdResult>
{
    public async Task<GetOrderByIdResult> Handle(GetOrdersByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.SingleOrDefaultAsync(_ => _.Id == OrderId.Of(query.Id));

        return new GetOrderByIdResult(order.ToOrderDto());
    }
}