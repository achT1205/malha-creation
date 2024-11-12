namespace Ordering.Application.Orders.Commands.ConfirmOrder;

public record SubmitForProcessingCommand(Guid Id) : ICommand<SubmitForProcessingResult>;
public record SubmitForProcessingResult(bool IsSuccess);
public class SubmitForProcessingCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<SubmitForProcessingCommand, SubmitForProcessingResult>
{
    public async Task<SubmitForProcessingResult> Handle(SubmitForProcessingCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.SubmitForProcessing();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new SubmitForProcessingResult(true);

    }
}
