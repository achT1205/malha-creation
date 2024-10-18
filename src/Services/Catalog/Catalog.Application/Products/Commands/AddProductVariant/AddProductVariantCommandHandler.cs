//using BuildingBlocks.CQRS;
//using Catalog.Application.Interfaces;
//using Catalog.Domain.ValueObjects;

//namespace Catalog.Application.Products.Commands.AddProductVariant;

//public class AddProductVariantCommandHandler : ICommandHandler<AddProductVariantCommand, AddProductVariantResult>
//{
//    private readonly IProductRepository _productRepository;
//    public AddProductVariantCommandHandler(IProductRepository productRepository)
//    {
//        _productRepository = productRepository;
//    }
//    public async Task<AddProductVariantResult> Handle(AddProductVariantCommand command, CancellationToken cancellationToken)
//    {
//        var product = await _productRepository.GetByIdAsync(ProductId.Of(command.ProductId));
//        var id = Guid.Empty;
//        if (command.ColorVariant is CreateAccessoryColorVariantDto accessoryVariant)
//        {
//            var accessoryColorVariant = ColorVariant.Create(
//                product.Id,
//                Color.Of(accessoryVariant.Color),
//                Slug.Of(product.UrlFriendlyName.Value, accessoryVariant.Color),
//                Price.Of(Currency.EUR, accessoryVariant.Price),
//                Quantity.Of(accessoryVariant.Quantity)
//            );
//            id = accessoryColorVariant.Id.Value;
//            product.AddColorVariant(accessoryColorVariant);
//        }
//        // Vérifier si c'est une variante de vêtement
//        //else if (command.ColorVariant is CreateClothingColorVariantDto clothingVariant)
//        //{

//        //    var clothingColorVariant = ColorVariant.Create(
//        //       product.Id,
//        //       Color.Of(clothingVariant.Color),
//        //       Slug.Of(product.UrlFriendlyName.Value, clothingVariant.Color),
//        //       images,
//        //       //Price.Of(0),
//        //       //Quantity.Of(0)
//        //   );
//        //    foreach (var sv in clothingVariant.SizeVariants)
//        //    {
//        //        var sizeVariant = SizeVariant.Create(Size.Of(sv.Size), Price.Of(sv.Price), Quantity.Of(sv.Quantity));
//        //        clothingColorVariant.AddSizeVariant(sizeVariant);
//        //    }

//        //    id = clothingColorVariant.Id.Value;
//        //    product.AddColorVariant(clothingColorVariant);
//        //}

//        await _productRepository.SaveChangesAsync();

//        return new AddProductVariantResult(id);
//    }
//}
