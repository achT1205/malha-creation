namespace Ordering.Application.Orders.Commands.ConfirmGracePeriod;

public record ConfirmGracePeriodCommand(Guid Id) : ICommand<ConfirmGracePeriodResult>;
public record ConfirmGracePeriodResult(bool IsSuccess);
public class ConfirmGracePeriodCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<ConfirmGracePeriodCommand, ConfirmGracePeriodResult>
{
    public async Task<ConfirmGracePeriodResult> Handle(ConfirmGracePeriodCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.ConfirmGracePeriod();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ConfirmGracePeriodResult(true);

    }
}
