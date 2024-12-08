namespace Ordering.Application.Orders.Commands.ProcessOrder;

public record ProcessOrderCommand(Guid Id) : ICommand<ProcessOrderResult>;
public record ProcessOrderResult(bool IsSuccess);
public class ProcessOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<ProcessOrderCommand, ProcessOrderResult>
{
    public async Task<ProcessOrderResult> Handle(ProcessOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.StartProcessing();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ProcessOrderResult(true);

    }
}
