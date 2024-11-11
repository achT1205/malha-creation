//namespace Catalog.Application.ProductTypes.Commands.DeleteProductType;

//public record DeleteProductTypeCommand(Guid Id) : ICommand<DeleteProductTypeResult>;
//public record DeleteProductTypeResult(bool IsSuccess);

//public class DeleteProductTypeCommandValidation : AbstractValidator<DeleteProductTypeCommand>
//{
//    public DeleteProductTypeCommandValidation()
//    {
//        RuleFor(x => x.Id).NotEmpty().WithMessage("ProductType Id is required");
//    }
//}
//public class DeleteProducCommandtHandler : ICommandHandler<DeleteProductTypeCommand, DeleteProductTypeResult>
//{
//    private readonly IProductTypeRepository _productTypeRepository;
//    public DeleteProducCommandtHandler(IProductTypeRepository productTypeRepository)
//    {
//        _productTypeRepository = productTypeRepository;
//    }

//    public async Task<DeleteProductTypeResult> Handle(DeleteProductTypeCommand command, CancellationToken cancellationToken)
//    {
//        var b = await _productTypeRepository.GetByIdAsync(ProductTypeId.Of(command.Id));
//        if (b != null)
//        {
//            await _productTypeRepository.RemoveAsync(b);
//            await _productTypeRepository.SaveChangesAsync();
//        }
//        return new DeleteProductTypeResult(true);
//    }
//}
