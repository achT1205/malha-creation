namespace Catalog.Application.Categories.Commands.CreateCategory;


public record CreateCategoryCommand(
    string Name
    )
    : ICommand<CreateCategoryResult>;
public record CreateCategoryResult(Guid Id);


public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Category name is required");
    }
}
public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
{
    private readonly ICategoryRepository _CategoryRepository;
    public CreateCategoryCommandHandler(ICategoryRepository CategoryRepository)
    {
        _CategoryRepository = CategoryRepository;
    }
    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = Category.Create(CategoryName.Of(command.Name));
        await _CategoryRepository.AddAsync(category);
        await _CategoryRepository.SaveChangesAsync();
        return new CreateCategoryResult(category.Id.Value);
    }
}
