using BuildingBlocks.Exceptions;
using Discount.Grpc;
using Stripe;

namespace Cart.API.Cart.Commands.Discount;
public record ApplyCartDiscountCommand(Guid UserId, string CouponCode) : ICommand<ApplyCartDiscountResult>;
public record ApplyCartDiscountResult(Guid UserId);

public class ApplyCartDiscountCommandValidator : AbstractValidator<ApplyCartDiscountCommand>
{
    public ApplyCartDiscountCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().WithMessage("UserId can not be null");
        RuleFor(x => x.CouponCode).NotNull().WithMessage("CouponCode is required");
    }
}
public class ApplyCartDiscountCommandHandler(
    ICartRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<ApplyCartDiscountCommand, ApplyCartDiscountResult>
{
    public async Task<ApplyCartDiscountResult> Handle(ApplyCartDiscountCommand command, CancellationToken cancellationToken)
    {

        var cart = await repository.GetCart(command.UserId, cancellationToken);
        if (cart == null)
        {
            throw new NotFoundException("Not Cat to checkout.");
        }

        var service = new CouponService();
        var stripeCoupon = await service.GetAsync(command.CouponCode);
        if (stripeCoupon != null)
        {

            var coupon = await discountProto.GetBasketDiscountAsync(
                new GetBasketDiscountRequest
                {
                    CouponCode = command.CouponCode,
                    CustomerId = command.UserId.ToString(),
                    OrderTotal = (double)cart.TotalPrice
                }, cancellationToken: cancellationToken);

            if (coupon != null && coupon.CouponCode != "None")
                cart.Coupon = coupon.Adapt<CouponModel>(); ;
        }



        await repository.StoreCart(cart, cancellationToken);

        return new ApplyCartDiscountResult(command.UserId);
    }
}