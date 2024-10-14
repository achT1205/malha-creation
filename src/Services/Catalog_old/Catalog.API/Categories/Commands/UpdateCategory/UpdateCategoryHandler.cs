namespace Catalog.API.Categories.Commands.UpdateCategory;
public record UpdateCategoryCommand(Category Category)
    : ICommand<UpdateCategoryResult>;
public record UpdateCategoryResult(bool IsSuccess);

public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidation()
    {
        RuleFor(x => x.Category.Id).NotEmpty().WithMessage("Category ID is required");
        RuleFor(x => x.Category.Name).NotEmpty().WithMessage("Category Name is required");
    }
}

public class UpdateCategoryCommandHandler(IDocumentSession session) : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
{
    public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await session.LoadAsync<Category>(command.Category.Id, cancellationToken);
        if (category == null)
        {
            throw new CategoryNotFoundException(command.Category.Id);
        }

        category.Name = command.Category.Name;
        category.Description = command.Category.Description;
        session.Update(category);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryResult(true);
    }
}