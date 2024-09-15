
namespace Catalog.API.Categories.Commands.DeleteCategory;
public record DeleteCategoryCommand(Guid Id):ICommand<DeleteCategoryResult>;
public record DeleteCategoryResult(bool IsSuccess);

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required");
    }
}
public class DeleteCategoryCommandHandler(IDocumentSession session) : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
{
    public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Category>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new CategoryNotFoundException(command.Id);
        }
        session.Delete<Category>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteCategoryResult(true);
    }
}