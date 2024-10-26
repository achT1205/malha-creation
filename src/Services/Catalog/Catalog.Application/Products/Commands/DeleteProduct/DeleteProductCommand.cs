using BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.DeleteProduct;
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;