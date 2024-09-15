namespace Catalog.API.Occasions.Commands.CreateOccasion;

public record CreateOccasionCommand(Occasion Occasion)
    : ICommand<CreateOccasionResult>;
public record CreateOccasionResult(Guid Id);

public class CreateOccasionCommandValidation : AbstractValidator<CreateOccasionCommand>
{
    public CreateOccasionCommandValidation()
    {
        RuleFor(x => x.Occasion.Name).NotEmpty().WithMessage("Name is required");
    }
}

public class CreateOccasionCommandHandler(IDocumentSession session) : ICommandHandler<CreateOccasionCommand, CreateOccasionResult>
{
    public async Task<CreateOccasionResult> Handle(CreateOccasionCommand command, CancellationToken cancellationToken)
    {
        var Occasion = new Occasion
        {
            Name = command.Occasion.Name,
            Description = command.Occasion.Description
        };

        session.Store(Occasion);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateOccasionResult(Occasion.Id);
    }
}