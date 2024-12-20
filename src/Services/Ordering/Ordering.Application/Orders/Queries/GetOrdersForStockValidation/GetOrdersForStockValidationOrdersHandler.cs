﻿namespace Ordering.Application.Orders.Queries.GetOrdersForStockValidation;

public record GetOrderForStockValidationQuery(Guid Id): IQuery<GetOrderForStockValidationResult>;

public record GetOrderForStockValidationResult(OrderStockDto Order);
public class GetOrderForStockValidationQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderForStockValidationQuery, GetOrderForStockValidationResult>
{
    public async Task<GetOrderForStockValidationResult> Handle(GetOrderForStockValidationQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(_=> _.Id == OrderId.Of(query.Id));

        return new GetOrderForStockValidationResult(order.ToOrderStockDto());
    }
}
