using Discount.Grpc;

namespace Cart.API.Cart.Commands.StoreCart;

public record StoreCartCommand(ShoppingCart Cart) : ICommand<StoreCartResult>;
public record StoreCartResult(Guid UserId);

public class StoreCartCommandValidator : AbstractValidator<StoreCartCommand>
{
    public StoreCartCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserId).NotNull().WithMessage("StoreCartHandler is required");
    }
}
public class StoreCartCommandHandler(ICartRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await DeductDiscount(command.Cart, cancellationToken);
            await repository.StoreCart(command.Cart, cancellationToken);

            return new StoreCartResult(command.Cart.UserId);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        // Communicate with Discount.Grpc and calculate lastest prices of products into sc
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductId = item.ProductId.ToString() }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}