namespace Catalog.Application.Products.Commands.UpdateSizeVariantPrice;

public record UpdateSizeVariantPriceCommand(Guid Id, Guid ColorVariantId, Guid SizeVariantId, decimal Price) : ICommand<UpdateSizeVariantPriceResult>;
public record UpdateSizeVariantPriceResult(bool IsSuccess);

public class UpdateSizeVariantPriceCommandValidation : AbstractValidator<UpdateSizeVariantPriceCommand>
{
    public UpdateSizeVariantPriceCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        RuleFor(x => x.ColorVariantId).NotEmpty().WithMessage("ColorVariantId is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}
public class UpdateSizeVariantPriceCommandHandler : ICommandHandler<UpdateSizeVariantPriceCommand, UpdateSizeVariantPriceResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateSizeVariantPriceCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<UpdateSizeVariantPriceResult> Handle(UpdateSizeVariantPriceCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.Id} was not found");
            }
            product.UpdateSizeVariantPrice(
                ColorVariantId.Of(command.ColorVariantId),
                SizeVariantId.Of(command.SizeVariantId),
                Price.Of("USD", command.Price)
                );
            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new UpdateSizeVariantPriceResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
