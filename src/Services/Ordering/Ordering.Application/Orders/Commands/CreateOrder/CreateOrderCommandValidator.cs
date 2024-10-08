namespace Ordering.Application.Orders.Commands.CreateOrder;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        RuleFor(x => x.BillingAddress).NotEmpty().WithMessage("BillingAddress is required");
        RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("ShippingAddress is required");
        RuleFor(x => x.Payment).NotEmpty().WithMessage("Payment is required");

    }
}