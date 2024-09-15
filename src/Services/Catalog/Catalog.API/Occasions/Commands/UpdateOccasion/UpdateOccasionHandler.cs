namespace Catalog.API.Occasions.Commands.UpdateOccasion;
public record UpdateOccasionCommand(Occasion Occasion)
    : ICommand<UpdateOccasionResult>;
public record UpdateOccasionResult(bool IsSuccess);

public class UpdateOccasionCommandValidation : AbstractValidator<UpdateOccasionCommand>
{
    public UpdateOccasionCommandValidation()
    {
        RuleFor(x => x.Occasion.Id).NotEmpty().WithMessage("Occasion ID is required");
        RuleFor(x => x.Occasion.Name).NotEmpty().WithMessage("Occasion Name is required");
    }
}

public class UpdateOccasionCommandHandler(IDocumentSession session) : ICommandHandler<UpdateOccasionCommand, UpdateOccasionResult>
{
    public async Task<UpdateOccasionResult> Handle(UpdateOccasionCommand command, CancellationToken cancellationToken)
    {
        var Occasion = await session.LoadAsync<Occasion>(command.Occasion.Id, cancellationToken);
        if (Occasion == null)
        {
            throw new OccasionNotFoundException(command.Occasion.Id);
        }

        Occasion.Name = command.Occasion.Name;
        Occasion.Description = command.Occasion.Description;
        session.Update(Occasion);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateOccasionResult(true);
    }
}