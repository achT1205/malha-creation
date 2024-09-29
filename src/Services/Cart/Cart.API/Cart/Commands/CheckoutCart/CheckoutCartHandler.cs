using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Cart.API.Cart.Commands.CheckoutCart;

public record CheckoutCartCommand(CartCheckoutDto CartCheckoutDto)
    : ICommand<CheckoutCartResult>;
public record CheckoutCartResult(bool IsSuccess);

public class CheckoutCartCommandValidator
    : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(x => x.CartCheckoutDto).NotNull().WithMessage("CartCheckoutDto can't be null");
        RuleFor(x => x.CartCheckoutDto.UserId).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutCartCommandHandler
    (ICartRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var Cart = await repository.GetCart(command.CartCheckoutDto.UserId, cancellationToken);
        if (Cart == null)
        {
            return new CheckoutCartResult(false);
        }

        var eventMessage = command.CartCheckoutDto.Adapt<CartCheckoutEvent>();
        eventMessage.TotalPrice = Cart.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteCart(command.CartCheckoutDto.UserId, cancellationToken);

        return new CheckoutCartResult(true);
    }
}