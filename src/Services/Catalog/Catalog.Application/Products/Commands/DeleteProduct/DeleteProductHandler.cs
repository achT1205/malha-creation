namespace Catalog.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidation : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
    }
}
public class DeleteProducCommandtHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _productRepository;
    public DeleteProducCommandtHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;

    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var p = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
        if (p != null)
        {
            _productRepository.RemoveAsync(p);
            await _productRepository.SaveChangesAsync();
        }
        return new DeleteProductResult(true);
    }
}
