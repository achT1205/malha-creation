namespace Ordering.Application.Orders.Queries.GetOrdersById;

public record GetOrdersByIdQuery(Guid Id)
    : IQuery<GetOrdersByIdResult>;
public record GetOrdersByIdResult(OrderDto Order);


public class GetOrdersByCodeQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByIdQuery, GetOrdersByIdResult>
{
    public async Task<GetOrdersByIdResult> Handle(GetOrdersByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(_ => _.Id == OrderId.Of(query.Id));

        return new GetOrdersByIdResult(order.ToOrderDto());
    }
}