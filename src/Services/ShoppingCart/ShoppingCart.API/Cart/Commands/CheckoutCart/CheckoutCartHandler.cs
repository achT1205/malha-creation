using MassTransit;
using ShoppingCart.API.Events.IntegrationEvent;

namespace Cart.API.Cart.Commands.CheckoutCart;

public record CheckoutCartCommand
    : ICommand<CheckoutCartResult>
{
    public Guid UserId { get; set; } = default!;

    // Shipping and BillingAddress
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;

    // Payment
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;
}
public record CheckoutCartResult(bool IsSuccess);

public class CheckoutCartCommandValidator
    : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("CartCheckoutDto can't be null");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutCartCommandHandler
    (ICartRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var Cart = await repository.GetCart(command.UserId, cancellationToken);
        if (Cart == null)
        {
            return new CheckoutCartResult(false);
        }

        var eventMessage = command.Adapt<CartCheckoutEvent>();
        eventMessage.Cart = Cart.Adapt<Basket>();

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteCart(command.UserId, cancellationToken);

        return new CheckoutCartResult(true);
    }
}