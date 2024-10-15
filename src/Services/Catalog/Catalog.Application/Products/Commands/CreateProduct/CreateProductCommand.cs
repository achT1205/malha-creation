using BuildingBlocks.CQRS;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : ICommand<CreateProductResult>
{
    public string Name { get; private set; } = default!;
    public string NameEn { get; private set; } = default!;
    public ImageId CoverImageId { get; private set; } = default!;
    public ProductTypeId ProductTypeId { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public MaterialId MaterialId { get; private set; } = default!;
    public bool IsHandmade { get; private set; }
    public CollectionId CollectionId { get; private set; } = default!;
}
