namespace Ordering.Application.Orders.Commands.ValidateRoder;

public record ValidateRoderCommand(Guid Id) : ICommand<ValidateRoderResult>;
public record ValidateRoderResult(bool IsSuccess);
public class ValidateRoderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<ValidateRoderCommand, ValidateRoderResult>
{
    public async Task<ValidateRoderResult> Handle(ValidateRoderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.ValidateOrder();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ValidateRoderResult(true);

    }
}
