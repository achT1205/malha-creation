namespace Ordering.Application.Orders.Commands.RejectedOrder;


public record RejectOrderCommand(Guid Id) : ICommand<RejectOrderResult>;
public record RejectOrderResult(bool IsSuccess);
public class RejectOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<RejectOrderCommand, RejectOrderResult>
{
    public async Task<RejectOrderResult> Handle(RejectOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.RejectOrder();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RejectOrderResult(true);

    }
}