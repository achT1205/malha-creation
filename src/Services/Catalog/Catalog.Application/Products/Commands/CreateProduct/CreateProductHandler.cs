using BuildingBlocks.CQRS;
using Catalog.Application.Data;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command);

        try
        {
            context.Products.Add(product);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private Product CreateNewProduct(CreateProductCommand command)
    {
        var newProduct = Product.Create(
            ProductId.Of(Guid.NewGuid()),
            command.Name,
            command.NameEn,
            command.CoverImageId,
            command.ProductTypeId,
            command.Description,
            command.MaterialId,
            command.IsHandmade,
            command.CollectionId);

        return newProduct;
    }
}
