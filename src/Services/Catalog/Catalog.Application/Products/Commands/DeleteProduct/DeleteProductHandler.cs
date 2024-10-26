using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Commands.DeleteProduct;

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
