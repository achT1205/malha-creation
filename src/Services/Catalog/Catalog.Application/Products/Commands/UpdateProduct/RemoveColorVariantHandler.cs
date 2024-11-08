namespace Catalog.Application.Products.Commands.RemoveColorVariant;

public record RemoveColorVariantCommand(Guid Id, Guid ColorVariantId) : ICommand<RemoveColorVariantResult>;
public record RemoveColorVariantResult(bool IsSuccess);

public class RemoveColorVariantCommandValidation : AbstractValidator<RemoveColorVariantCommand>
{
    public RemoveColorVariantCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
    }
}
public class RemoveColorVariantCommandHandler : ICommandHandler<RemoveColorVariantCommand, RemoveColorVariantResult>
{
    private readonly IProductRepository _productRepository;

    public RemoveColorVariantCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<RemoveColorVariantResult> Handle(RemoveColorVariantCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            product.RemoveColorVariant(ColorVariantId.Of(command.ColorVariantId));
            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new RemoveColorVariantResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
