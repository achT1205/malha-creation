namespace Catalog.Application.Collections.Commands.CreateCollection;


public record CreateCollectionCommand(string Name, string Description, ImageDto CoverImage)
    : ICommand<CreateCollectionResult>;
public record CreateCollectionResult(Guid Id);


public class CreateCollectionCommandValidator : AbstractValidator<CreateCollectionCommand>
{
    public CreateCollectionCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Collection name is required");
    }
}
public class CreateCollectionCommandHandler : ICommandHandler<CreateCollectionCommand, CreateCollectionResult>
{
    private readonly ICollectionRepository _CollectionRepository;
    public CreateCollectionCommandHandler(ICollectionRepository CollectionRepository)
    {
        _CollectionRepository = CollectionRepository;
    }
    public async Task<CreateCollectionResult> Handle(CreateCollectionCommand command, CancellationToken cancellationToken)
    {
        var image = Image.Of(command.CoverImage.ImageSrc, command.CoverImage.AltText);
        var collection = Collection.Create(CollectionId.Of(Guid.NewGuid()), command.Name, command.Description, image);
        await _CollectionRepository.AddAsync(collection);
        await _CollectionRepository.SaveChangesAsync();
        return new CreateCollectionResult(collection.Id.Value);
    }
}
