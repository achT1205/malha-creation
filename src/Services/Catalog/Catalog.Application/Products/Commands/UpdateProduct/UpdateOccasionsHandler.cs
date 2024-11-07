using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;

namespace Catalog.Application.Products.Commands.UpdateOccasions;

public record UpdateOccasionsCommand(Guid Id, List<Guid> OccasionIds) : ICommand<UpdateOccasionsResult>;
public record UpdateOccasionsResult(bool IsSuccess);

public class UpdateOccasionsCommandValidation : AbstractValidator<UpdateOccasionsCommand>
{
    public UpdateOccasionsCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.OccasionIds).NotEmpty().WithMessage("Occasions are required");
    }
}
public class UpdateOccasionsCommandHandler : ICommandHandler<UpdateOccasionsCommand, UpdateOccasionsResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateOccasionsCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<UpdateOccasionsResult> Handle(UpdateOccasionsCommand command, CancellationToken cancellationToken)
    {

        try
        {
            var product = await _productRepository.GetByIdAsync(ProductId.Of(command.Id));
            product.RemoveOccasions(product.OccasionIds.ToList());
            var ids = command.OccasionIds.Select(id => OccasionId.Of(id)).ToList();
            product.AddOccasions(ids);

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new UpdateOccasionsResult(true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
