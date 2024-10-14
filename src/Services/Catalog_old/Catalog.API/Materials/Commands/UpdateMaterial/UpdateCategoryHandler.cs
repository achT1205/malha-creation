namespace Catalog.API.Materials.Commands.UpdateMaterial;
public record UpdateMaterialCommand(Material Material)
    : ICommand<UpdateMaterialResult>;
public record UpdateMaterialResult(bool IsSuccess);

public class UpdateMaterialCommandValidation : AbstractValidator<UpdateMaterialCommand>
{
    public UpdateMaterialCommandValidation()
    {
        RuleFor(x => x.Material.Id).NotEmpty().WithMessage("Material ID is required");
        RuleFor(x => x.Material.Name).NotEmpty().WithMessage("Material Name is required");
    }
}

public class UpdateMaterialCommandHandler(IDocumentSession session) : ICommandHandler<UpdateMaterialCommand, UpdateMaterialResult>
{
    public async Task<UpdateMaterialResult> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
    {
        var Material = await session.LoadAsync<Material>(command.Material.Id, cancellationToken);
        if (Material == null)
        {
            throw new MaterialNotFoundException(command.Material.Id);
        }

        Material.Name = command.Material.Name;
        Material.Description = command.Material.Description;
        session.Update(Material);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateMaterialResult(true);
    }
}