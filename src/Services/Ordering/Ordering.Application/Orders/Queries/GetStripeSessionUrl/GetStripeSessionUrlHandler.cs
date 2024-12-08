using Stripe.Checkout;

namespace Ordering.Application.Orders.Queries.GetStripeSessionUrl;



public record GetStripeSessionUrlQuery(Guid OrderId) : IQuery<GetStripeSessionUrlQueryResult>;

public record GetStripeSessionUrlQueryResult(string Url);

public class GetStripeSessionUrlQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetStripeSessionUrlQuery, GetStripeSessionUrlQueryResult>
{
    public async Task<GetStripeSessionUrlQueryResult> Handle(GetStripeSessionUrlQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var order = await dbContext.Orders
                .SingleOrDefaultAsync(t => t.Id == OrderId.Of(query.OrderId), cancellationToken);
            if (order is null)
            {
                throw new OrderNotFoundException(query.OrderId);
            }

            var service = new SessionService();
            Session session = await service.GetAsync(order.StripeSessionId);
            return new GetStripeSessionUrlQueryResult(session.Url);
        }
        catch (Exception ex)
        {
            throw;
        }


    }
}
