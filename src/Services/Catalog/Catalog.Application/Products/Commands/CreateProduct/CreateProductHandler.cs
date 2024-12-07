﻿using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.Events;
using Catalog.Application.Extensions;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using Mapster;
using MassTransit;

namespace Catalog.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{

    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IPublishEndpoint publishEndpoint
        )
    {
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command);
        try
        {

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();


            var dto  = product.ToProductDto(null, null, null, null, null);
            var eventMessage = dto.Adapt<ProductCreatedEvent>();
            await _publishEndpoint.Publish(eventMessage, cancellationToken);

        }
        catch (Exception ex)
        {
            throw new InternalServerException(ex.InnerException.Message);
        }

        return new CreateProductResult(product.Id.Value);
    }

    private Product CreateNewProduct(CreateProductCommand command)
    {
        var coverImage = Image.Of(command.CoverImage.ImageSrc, command.CoverImage.AltText);
        var product = Product.Create(
            ProductId.Of(Guid.NewGuid()),
            ProductName.Of(command.Name),
            UrlFriendlyName.Of(command.UrlFriendlyName),
            ProductDescription.Of(command.Description),
            command.IsHandmade,
            coverImage,
            ProductTypeId.Of(command.ProductTypeId),
            command.ProductType,
            MaterialId.Of(command.MaterialId),
            CollectionId.Of(command.CollectionId),
            AverageRating.Of(0m, 0)
        );

        foreach (var id in command.CategoryIds)
        {
            product.AddCategory(CategoryId.Of(id));
        }

        foreach (var id in command.OccasionIds)
        {
            product.AddOccasion(OccasionId.Of(id));
        }
        foreach (var colorVariant in command.ColorVariants)
            {

                var newColorVariant = ColorVariant.Create(
                    product.Id,
                    Color.Of(colorVariant.Color),
                    Slug.Of(command.UrlFriendlyName, colorVariant.Color),
                    ColorVariantPrice.Of(colorVariant.Price.HasValue ? "USD" : null, colorVariant.Price),
                    ColorVariantQuantity.Of(colorVariant.Quantity));

                foreach (var image in colorVariant.Images)
                {
                    var newImage = Image.Of(image.ImageSrc, image.AltText);
                    newColorVariant.AddImage(newImage);
                }

                foreach (var sizeVariant in colorVariant.sizeVariants)
                {
                    var newSizeVariant = SizeVariant.Create(
                        newColorVariant.Id,
                        SizeVariantId.Of(Guid.NewGuid()),
                        Size.Of(sizeVariant.Size),
                        Price.Of("USD", sizeVariant.Price),
                    Quantity.Of(sizeVariant.Quantity));
                    newColorVariant.AddSizeVariant(newSizeVariant);
                }
                product.AddColorVariant(newColorVariant);
            }

        return product;

    }
}
