using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.Events;
using MassTransit;

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
    public string City { get; set; } = default!;
    public string ZipCode { get; set; } = default!;

    // Payment
    public string CardHolderName { get; set; } = default!;
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
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("EmailAddress is required");
        RuleFor(x => x.AddressLine).NotEmpty().WithMessage("AddressLine is required");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required");
        RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("CardHolderName is required");
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required");
        RuleFor(x => x.Expiration).NotEmpty().WithMessage("Expiration is required");
        RuleFor(x => x.CVV).NotEmpty().WithMessage("CVV is required");
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
        if(cart.Checkout)
            throw new BadHttpRequestException("Checkout in progress");


        var eventMessage = command.Adapt<CartCheckoutEvent>();

        await publishEndpoint.Publish(eventMessage, cancellationToken);
        cart.Checkout = true;
        await repository.StoreCart(cart, cancellationToken);

        //await repository.DeleteCart(command.UserId, cancellationToken);

        return new CheckoutCartResult(true);
    }
}