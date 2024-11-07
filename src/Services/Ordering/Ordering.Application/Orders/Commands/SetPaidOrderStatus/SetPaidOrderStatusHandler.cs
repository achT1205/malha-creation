namespace Ordering.Application.Orders.Commands.SetPaidOrderStatus;


public record SetPaidOrderStatusCommand(Guid Id) : ICommand<SetPaidOrderStatusResult>;
public record SetPaidOrderStatusResult(bool IsSuccess);
public class SetPaidOrderStatusCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<SetPaidOrderStatusCommand, SetPaidOrderStatusResult>
{
    public async Task<SetPaidOrderStatusResult> Handle(SetPaidOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.MarkAsPaid();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new SetPaidOrderStatusResult(true);

    }
}

