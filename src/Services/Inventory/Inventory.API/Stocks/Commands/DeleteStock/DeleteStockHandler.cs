
namespace Inventory.API.Stocks.Commands.DeleteStock;
public record DeleteStockCommand(Guid Id) : ICommand<DeleteStockResult>;
public record DeleteStockResult(bool IsSuccess);

public class DeleteStockCommandValidator : AbstractValidator<DeleteStockCommand>
{
    public DeleteStockCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Stock ID is required");
    }
}
public class DeleteStockCommandHandler(IDocumentSession session) : ICommandHandler<DeleteStockCommand, DeleteStockResult>
{
    public async Task<DeleteStockResult> Handle(DeleteStockCommand command, CancellationToken cancellationToken)
    {
        var stock = await session.LoadAsync<Stock>(command.Id, cancellationToken);
        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ID {command.Id} is not found");
        }

        session.Delete<Stock>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteStockResult(true);
    }
}