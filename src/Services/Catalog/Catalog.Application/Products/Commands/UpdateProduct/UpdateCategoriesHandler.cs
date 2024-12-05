namespace Catalog.Application.Products.Commands.UpdateCategories;

public record UpdateCategoriesCommand(Guid Id, List<Guid> CategoryIds) : ICommand<UpdateCategoriesResult>;
public record UpdateCategoriesResult(bool IsSuccess);

public class UpdateCategoriesCommandValidation : AbstractValidator<UpdateCategoriesCommand>
{
    public UpdateCategoriesCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
    }
}
public class UpdateCategoriesCommandHandler : ICommandHandler<UpdateCategoriesCommand, UpdateCategoriesResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateCategoriesCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<UpdateCategoriesResult> Handle(UpdateCategoriesCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }
            product.RemoveCategories(product.CategoryIds.ToList());
            var ids = command.CategoryIds.Select(id => CategoryId.Of(id)).ToList();
            product.AddCategories(ids);

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new UpdateCategoriesResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
