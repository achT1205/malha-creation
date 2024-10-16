using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IOccasionRepository _occasionRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly ICollectionRepository _collectionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(
        IProductTypeRepository productTypeRepository,
        IOccasionRepository occasionRepository,
        IMaterialRepository materialRepository,
        ICollectionRepository collectionRepository,
        ICategoryRepository categoryRepository,
        IImageRepository imageRepository,
        IProductRepository productRepository)
    {
        _productTypeRepository = productTypeRepository;
        _occasionRepository = occasionRepository;
        _materialRepository = materialRepository;
        _collectionRepository = collectionRepository;
        _categoryRepository = categoryRepository;
        _imageRepository = imageRepository;
        _productRepository = productRepository;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Récupérer les objets du domaine via les repositories
        var productType = await _productTypeRepository.GetByIdAsync(ProductTypeId.Of(command.ProductTypeId));
        var material = await _materialRepository.GetByIdAsync(MaterialId.Of(command.MaterialId));
        var collection = await _collectionRepository.GetByIdAsync(CollectionId.Of(command.CollectionId));
        var coverImage = await _imageRepository.GetByIdAsync(ImageId.Of(command.CoverImageId));

        // Récupérer les occasions et catégories
        var occasion = await _occasionRepository.GetByIdAsync(OccasionId.Of(command.OccasionIds.First()));
        var occasions = await _occasionRepository.GetByIdsAsync(command.OccasionIds.Select(OccasionId.Of).ToList());
        var categories = await _categoryRepository.GetByIdsAsync(command.CategoryIds.Select(CategoryId.Of).ToList());

        // Créer le produit en utilisant les objets du domaine
        var product = Product.Create(
            command.Name,
            command.UrlFriendlyName,
            command.Description,
            command.IsHandmade,
            coverImage, // Image récupérée depuis le repository
            productType, // Objet ProductType
            occasion, // Une seule occasion ou la principale
            material, // Objet Material
            collection, // Objet Collection
            occasions, // Liste d'occasions
            categories // Liste de catégories
        );

        // Sauvegarder le produit dans le repository (non montré ici)
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return new CreateProductResult(product.Id.Value);
    }
}
