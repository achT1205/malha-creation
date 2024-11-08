namespace Ordering.Application.Orders.Commands.ValidationOrder;

public record ValidationOrderCommand(Guid Id) : ICommand<ValidationOrderResult>;
public record ValidationOrderResult(bool IsSuccess);
public class ValidationOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<ValidationOrderCommand, ValidationOrderResult>
{
    public async Task<ValidationOrderResult> Handle(ValidationOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.ValidateOrder();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ValidationOrderResult(true);

    }
}
