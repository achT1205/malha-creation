namespace Catalog.API.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(Category Category)
    : ICommand<CreateCategoryResult>;
public record CreateCategoryResult(Guid Id);

public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(x => x.Category.Name).NotEmpty().WithMessage("Name is required");
    }
}

public class CreateCategoryCommandHandler(IDocumentSession session) : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
{
    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = command.Category.Name,
            Description = command.Category.Description
        };

        session.Store(category);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateCategoryResult(category.Id);
    }
}