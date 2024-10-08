namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.BillingAddress).NotEmpty().WithMessage("BillingAddress is required");
        RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("ShippingAddress is required");
        RuleFor(x => x.Payment).NotEmpty().WithMessage("Payment is required");
    }
}