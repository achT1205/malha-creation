namespace Catalog.Application.Materials.Commands.DeleteMaterial;

public record DeleteMaterialCommand(Guid Id) : ICommand<DeleteMaterialResult>;
public record DeleteMaterialResult(bool IsSuccess);

public class DeleteMaterialCommandValidation : AbstractValidator<DeleteMaterialCommand>
{
    public DeleteMaterialCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Material Id is required");
    }
}
public class DeleteProducCommandtHandler : ICommandHandler<DeleteMaterialCommand, DeleteMaterialResult>
{
    private readonly IMaterialRepository  _materialRepository ;
    public DeleteProducCommandtHandler(IMaterialRepository materialRepository )
    {
        _materialRepository = materialRepository;

    }

    public async Task<DeleteMaterialResult> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken)
    {
        var b = await _materialRepository.GetByIdAsync(MaterialId.Of(command.Id));
        if (b != null)
        {
           await _materialRepository.RemoveAsync(b);
            await _materialRepository.SaveChangesAsync();
        }
        return new DeleteMaterialResult(true);
    }
}
