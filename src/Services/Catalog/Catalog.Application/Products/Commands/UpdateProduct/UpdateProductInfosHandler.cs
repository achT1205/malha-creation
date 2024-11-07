using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;

namespace Catalog.Application.Products.Commands.UpdateProductInfos;

public record UpdateProductInfosCommand(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    ImageDto CoverImage,
    Guid MaterialId,
    Guid BrandId,
    Guid CollectionId
   )
    : ICommand<UpdateProductInfosResult>;
public record UpdateProductInfosResult(bool IsSuccess);
public record RemovedItems(
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<Guid> ColorVariantIds
    );

public class UpdateProductInfosCommandValidation : AbstractValidator<UpdateProductInfosCommand>
{
    public UpdateProductInfosCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.UrlFriendlyName).NotEmpty().WithMessage("UrlFriendlyName is required.");
        RuleFor(x => x.UrlFriendlyName)
            .Matches(@"^[a-zA-Z0-9 \-]*$")
            .WithMessage("The field must not contain special characters.");
    }
}
public class UpdateProductInfosCommandHandler : ICommandHandler<UpdateProductInfosCommand, UpdateProductInfosResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductInfosCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<UpdateProductInfosResult> Handle(UpdateProductInfosCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            var updatedProduct = UpdateProductInfosEntity(product, command);
            _productRepository.UpdateAsync(updatedProduct);
            await _productRepository.SaveChangesAsync();

            return new UpdateProductInfosResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private Product UpdateProductInfosEntity(Product product, UpdateProductInfosCommand command)
    {
        product.UpdateNames(ProductName.Of(command.Name), UrlFriendlyName.Of(command.UrlFriendlyName));
        product.UpdateDescription(ProductDescription.Of(command.Description)); 
        product.UpdateMaterial(MaterialId.Of(command.MaterialId));
        product.UpdateCollection(CollectionId.Of(command.CollectionId));
        product.UpdateBrand(BrandId.Of(command.BrandId));
        product.UpdateHandMade(command.IsHandmade);
        product.UpdateCoverImage(Image.Of(command.CoverImage.ImageSrc, command.CoverImage.AltText));

        return product;
    }
}
