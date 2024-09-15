namespace Catalog.API.Products.Commands.UpdateProduct;
public record UpdateProductCommand(Product Product)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.ProductType).NotEmpty().WithMessage("ProductType is required");
        RuleFor(x => x.Product.Categories).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Product.CoverImage).NotEmpty().WithMessage("CoverImage is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var Product = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);
        if (Product == null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        Product.Name = command.Product.Name;
        Product.CoverImage = command.Product.CoverImage;
        Product.ProductType = command.Product.ProductType;
        Product.ForOccasion = command.Product.ForOccasion;
        Product.Description = command.Product.Description;
        Product.Material = command.Product.Material;
        Product.IsHandmade = command.Product.IsHandmade;
        Product.UpdatedAt = DateTime.Now;
        Product.Collection = command.Product.Collection;
        Product.Categories = command.Product.Categories;
        Product.Price = command.Product.Price;
        Product.Colors = command.Product.Colors;
        Product.Sizes = command.Product.Sizes;
        session.Update(Product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}