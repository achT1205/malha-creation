namespace Ordering.Application.Orders.Commands.CreateOrder;

public record AutoCreateOrderCommand (Order Order): ICommand<CreateOrderResult>;


public class AutoCreateOrderCommandValidator : AbstractValidator<AutoCreateOrderCommand>
{
    public AutoCreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull().WithMessage("Order is required");
    }
}

public class AutoCreateOrderCommandHandler(
    ILogger<AutoCreateOrderCommandHandler> _logger,
    IApplicationDbContext _context)
    : ICommandHandler<AutoCreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(AutoCreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = command.Order;

        _logger.LogInformation("Creating Order - Order: {@Order}", order);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

   
}