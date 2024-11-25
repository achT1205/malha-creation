namespace Ordering.Application.Orders.Commands.PackOrder;

public record PackOrderCommand(Guid Id) : ICommand<PackOrderResult>;
public record PackOrderResult(bool IsSuccess);
public class PackOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<PackOrderCommand, PackOrderResult>
{
    public async Task<PackOrderResult> Handle(PackOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.StartPacking();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new PackOrderResult(true);
    }
}
