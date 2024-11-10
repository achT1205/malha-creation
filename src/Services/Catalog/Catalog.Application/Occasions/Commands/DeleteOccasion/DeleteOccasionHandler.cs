namespace Catalog.Application.Occasions.Commands.DeleteOccasion;

public record DeleteOccasionCommand(Guid Id) : ICommand<DeleteOccasionResult>;
public record DeleteOccasionResult(bool IsSuccess);

public class DeleteOccasionCommandValidation : AbstractValidator<DeleteOccasionCommand>
{
    public DeleteOccasionCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Occasion Id is required");
    }
}
public class DeleteProducCommandtHandler : ICommandHandler<DeleteOccasionCommand, DeleteOccasionResult>
{
    private readonly IOccasionRepository  _occasionRepository ;
    public DeleteProducCommandtHandler(IOccasionRepository occasionRepository )
    {
        _occasionRepository = occasionRepository;

    }

    public async Task<DeleteOccasionResult> Handle(DeleteOccasionCommand command, CancellationToken cancellationToken)
    {
        var b = await _occasionRepository.GetByIdAsync(OccasionId.Of(command.Id));
        if (b != null)
        {
           await _occasionRepository.RemoveAsync(b);
            await _occasionRepository.SaveChangesAsync();
        }
        return new DeleteOccasionResult(true);
    }
}
