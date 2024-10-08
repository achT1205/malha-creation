namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {

        var order = await dbContext.Orders
             .SingleOrDefaultAsync(o => o.Id.Value == command.OrderId, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        dbContext.Orders.Remove(order);

        await dbContext.SaveChangesAsync(cancellationToken);


        return new DeleteOrderResult(true);
    }
}