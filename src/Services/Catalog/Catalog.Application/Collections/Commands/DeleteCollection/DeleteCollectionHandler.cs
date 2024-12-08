namespace Catalog.Application.Collections.Commands.DeleteCollection;

public record DeleteCollectionCommand(Guid Id) : ICommand<DeleteCollectionResult>;
public record DeleteCollectionResult(bool IsSuccess);

public class DeleteCollectionCommandValidation : AbstractValidator<DeleteCollectionCommand>
{
    public DeleteCollectionCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Collection Id is required");
    }
}
public class DeleteProducCommandtHandler : ICommandHandler<DeleteCollectionCommand, DeleteCollectionResult>
{
    private readonly ICollectionRepository  _collectionRepository ;
    public DeleteProducCommandtHandler(ICollectionRepository collectionRepository )
    {
        _collectionRepository = collectionRepository;

    }

    public async Task<DeleteCollectionResult> Handle(DeleteCollectionCommand command, CancellationToken cancellationToken)
    {
        var b = await _collectionRepository.GetByIdAsync(CollectionId.Of(command.Id));
        if (b != null)
        {
           await _collectionRepository.RemoveAsync(b);
            await _collectionRepository.SaveChangesAsync();
        }
        return new DeleteCollectionResult(true);
    }
}
