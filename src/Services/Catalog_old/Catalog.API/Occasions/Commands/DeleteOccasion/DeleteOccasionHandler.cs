
namespace Catalog.API.Occasions.Commands.DeleteOccasion;
public record DeleteOccasionCommand(Guid Id):ICommand<DeleteOccasionResult>;
public record DeleteOccasionResult(bool IsSuccess);

public class DeleteOccasionCommandValidator : AbstractValidator<DeleteOccasionCommand>
{
    public DeleteOccasionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Occasion ID is required");
    }
}
public class DeleteOccasionCommandHandler(IDocumentSession session) : ICommandHandler<DeleteOccasionCommand, DeleteOccasionResult>
{
    public async Task<DeleteOccasionResult> Handle(DeleteOccasionCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Occasion>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new OccasionNotFoundException(command.Id);
        }
        session.Delete<Occasion>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteOccasionResult(true);
    }
}