namespace Ordering.Application.Orders.Commands.CancelOrder;

public record CancelOrderCommand(Guid Id) : ICommand<CancelOrderResult>;
public record CancelOrderResult(bool IsSuccess);
public class CancelOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CancelOrderCommand, CancelOrderResult>
{
    public async Task<CancelOrderResult> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.CancelOrder();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CancelOrderResult(true);

    }
}
