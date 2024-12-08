namespace Ordering.Application.Orders.Commands.ShipOrder;


public record ShipOrderCommad(Guid Id) : ICommand<ShipOrderResult>;
public record ShipOrderResult(bool IsSuccess);
public class ShipOrderCommadHandler(IApplicationDbContext dbContext) : ICommandHandler<ShipOrderCommad, ShipOrderResult>
{
    public async Task<ShipOrderResult> Handle(ShipOrderCommad command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.ShipOrder();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ShipOrderResult(true);

    }
}