using BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    Guid ProductTypeId, 
    Guid MaterialId,    
    Guid CollectionId,  
    Guid CoverImageId,  
    List<Guid> CategoryIds,   
    List<Guid> OccasionIds
   )
    : ICommand<CreateProductResult>;
