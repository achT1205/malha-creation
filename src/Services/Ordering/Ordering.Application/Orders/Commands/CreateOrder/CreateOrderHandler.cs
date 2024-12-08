namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand (Order Order): ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull().WithMessage("Order is required");
    }
}

public class CreateOrderCommandHandler(
    ILogger<CreateOrderCommandHandler> _logger,
    IApplicationDbContext _context)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = command.Order;

        _logger.LogInformation("Creating Order - Order: {@Order}", order);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }
}