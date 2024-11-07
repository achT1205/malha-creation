namespace Cart.API.Cart.Queries.GetCart;

public record GetCartQuery(Guid UserId) : IQuery<GetCartResult>;
public record GetCartResult(Basket Cart);
internal class GetCartQueryHandler(ICartRepository repository) : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var Cart = await repository.GetCart(query.UserId);
        return new GetCartResult(Cart);
    }
}