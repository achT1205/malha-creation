namespace Catalog.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : ICommand<DeleteCategoryResult>;
public record DeleteCategoryResult(bool IsSuccess);

public class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Category Id is required");
    }
}
public class DeleteProducCommandtHandler : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
{
    private readonly ICategoryRepository  _categoryRepository ;
    public DeleteProducCommandtHandler(ICategoryRepository categoryRepository )
    {
        _categoryRepository = categoryRepository;

    }

    public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var b = await _categoryRepository.GetByIdAsync(CategoryId.Of(command.Id));
        if (b != null)
        {
           await _categoryRepository.RemoveAsync(b);
            await _categoryRepository.SaveChangesAsync();
        }
        return new DeleteCategoryResult(true);
    }
}
