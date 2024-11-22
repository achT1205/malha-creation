namespace Cart.API.Cart.Commands.Discount;
public record ApplyCartDiscountCommand(ApplyCartDiscountDto DiscountDto) : ICommand<ApplyCartDiscountResult>;
public record ApplyCartDiscountResult(Guid UserId);

public class ApplyCartDiscountCommandValidator : AbstractValidator<ApplyCartDiscountCommand>
{
    public ApplyCartDiscountCommandValidator()
    {
        RuleFor(x => x.DiscountDto.UserId).NotNull().WithMessage("UserId can not be null");
        RuleFor(x => x.DiscountDto.CouponCode).NotNull().WithMessage("CouponCode is required");
    }
}
public class ApplyCartDiscountCommandHandler(
    ICartRepository repositor
    /*CartDiscountProtoService.CartDiscountProtoServiceClient cartDiscountProto*/) : ICommandHandler<ApplyCartDiscountCommand, ApplyCartDiscountResult>
{
    public async Task<ApplyCartDiscountResult> Handle(ApplyCartDiscountCommand command, CancellationToken cancellationToken)
    {
        //var coupon = await cartDiscountProto.GetCartDiscountAsync(new GetCartDiscountRequest { CouponCode = command.DiscountDto.CouponCode }, cancellationToken: cancellationToken);
        //var cart = await repository.GetCart(command.DiscountDto.UserId);

        //var price = cart.TotalPrice - (cart.TotalPrice * coupon.DiscountRate) / 100;

        ////cart.SetTotalPrice(price);

        //await repository.StoreCart(cart, cancellationToken);

        return new ApplyCartDiscountResult(command.DiscountDto.UserId);
    }
}