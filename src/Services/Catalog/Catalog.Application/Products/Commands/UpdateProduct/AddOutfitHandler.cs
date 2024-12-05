namespace Catalog.Application.Products.Commands.AddOutfit;

public record AddOutfitCommand(Guid ProductId, Guid ColorVariantId, Guid OutfitId) : ICommand<AddOutfitResult>;
public record AddOutfitResult(bool IsSuccess);

public class AddOutfitCommandValidation : AbstractValidator<AddOutfitCommand>
{
    public AddOutfitCommandValidation()
    {

        RuleFor(x => x.ColorVariantId).NotEmpty().WithMessage("ColorVariantId is required");
        RuleFor(x => x.OutfitId).NotEmpty().WithMessage("OutfitId is required");
    }
}
public class AddOutfitCommandHandler : ICommandHandler<AddOutfitCommand, AddOutfitResult>
{
    private readonly IColorVariantRepository _colorVariantRepository;
    private readonly IProductRepository _productRepository;
    public AddOutfitCommandHandler(IProductRepository productRepository, IColorVariantRepository colorVariantRepository)
    {
        _colorVariantRepository = colorVariantRepository;
        _productRepository = productRepository; 
    }
    public async Task<AddOutfitResult> Handle(AddOutfitCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.ProductId));
            if (product == null)
            {
                throw new NotFoundException($"The product {command.ProductId} was not found");
            }
            var outfit = await _colorVariantRepository.GetByIdAsync(ColorVariantId.Of(command.OutfitId));
            if (outfit == null)
            {
                throw new NotFoundException($"The ColorVariant {command.OutfitId} was not found");
            }
            product.AddOutfit(ColorVariantId.Of(command.ColorVariantId),ColorVariantId.Of(command.OutfitId));

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new AddOutfitResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
