﻿using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using FluentValidation;

namespace Catalog.Application.Materials.Commands.CreateMaterial;


public record CreateMaterialCommand(string Name) : ICommand<CreateMaterialResult>;
public record CreateMaterialResult(Guid Id);


public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    public CreateMaterialCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Material name is required");
    }
}
public class CreateMaterialCommandHandler : ICommandHandler<CreateMaterialCommand, CreateMaterialResult>
{
    private readonly IMaterialRepository _materialRepository;
    public CreateMaterialCommandHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }
    public async Task<CreateMaterialResult> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
    {
        var type = Material.Create(command.Name);
        await _materialRepository.AddAsync(type);
        await _materialRepository.SaveChangesAsync();
        return new CreateMaterialResult(type.Id.Value);
    }
}
