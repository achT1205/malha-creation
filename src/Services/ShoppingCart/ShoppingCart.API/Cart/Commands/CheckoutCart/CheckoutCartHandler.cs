using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Cart.API.Cart.Commands.CheckoutCart;

public record CheckoutCartCommand
    : ICommand<CheckoutCartResult>
{
    public Guid UserId { get; set; } = default!;

    public AddressDto ShippingAddress { get; set; } = default!;
    public AddressDto BillingAddress { get; set; } = default!;
    public PaymentDto Payment { get; set; } = default!;

}
public record CheckoutCartResult(bool IsSuccess);

public class CheckoutCartCommandValidator
    : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("CartCheckoutDto can't be null");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserName is required");
        //RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
        //RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
        //RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("EmailAddress is required");
        //RuleFor(x => x.AddressLine).NotEmpty().WithMessage("AddressLine is required");
        //RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
        //RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
        //RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required");
        //RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("CardHolderName is required");
        //RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required");
        //RuleFor(x => x.Expiration).NotEmpty().WithMessage("Expiration is required");
        //RuleFor(x => x.CVV).NotEmpty().WithMessage("CVV is required");
    }
}

public class CheckoutCartCommandHandler
    (ICartRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetCart(command.UserId, cancellationToken);
        if (cart == null)
        {
            throw new NotFoundException("Not Cat to checkout.");
        }

        var eventMessage = command.Adapt<CartCheckoutEvent>();
        eventMessage.Basket = cart.Adapt<OrderBasket>();

        await publishEndpoint.Publish(eventMessage, cancellationToken);


        await repository.StoreCart(cart, cancellationToken);

        await repository.DeleteCart(command.UserId, cancellationToken);

        return new CheckoutCartResult(true);
    }
}