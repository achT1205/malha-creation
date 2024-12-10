using BuildingBlocks.Pagination;
using Discount.Grpc;
using Mapster;

namespace Catalog.Application.Products.Queries.GetLiteProducts;

public record GetLiteProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetLiteProductsQueryResult>;
public record GetLiteProductsQueryResult(PaginatedResult<LiteProduct> Products);

public record LiteProduct(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    ProductType ProductType,
    string ProductTypeString,
    ImageDto CoverImage,
    Coupon? Discount,
    List<LiteColorVariant> ColorVariants);
public record LiteColorVariant(
    Guid Id,
    string Color,
    string Background,
    string Class, ImageDto Image,
    decimal? Price,
    decimal? DiscountedPrice,
    string? InventoryStatus,
    string Slug,
    List<LiteSizeVariant>? SizeVariants);
public record LiteSizeVariant(
    Guid Id,
    string Size,
    decimal Price,
    decimal DiscountedPrice,
    string InventoryStatus);

public enum InventoryStatus
{
    INSTOCK,
    LOWSTOCK,
    OUTOFSTOCK
}

public class Coupon
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal FlatAmount { get; set; }
    public int Percentage { get; set; }

}


public class GetProductWithDetailsQueryHandler : IQueryHandler<GetLiteProductsQuery, GetLiteProductsQueryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProto;

    public GetProductWithDetailsQueryHandler(IProductRepository productRepository, DiscountProtoService.DiscountProtoServiceClient discountProto)
    {
        _productRepository = productRepository;
        _discountProto = discountProto;
    }
    public async Task<GetLiteProductsQueryResult> Handle(GetLiteProductsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var (products, totalCount) = await _productRepository.GetAllAsync();

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var liteProducts = new List<LiteProduct>();

            foreach (var p in products)
            {
                var lite = await MapProduct(p, _discountProto, cancellationToken);
                liteProducts.Add(lite);
            }

            return new GetLiteProductsQueryResult(
                new PaginatedResult<LiteProduct>(
                pageIndex,
                pageSize,
                totalCount,
                liteProducts)
                );
        }
        catch (Exception ex)
        {

            throw;
        }
    }



    private async Task<LiteProduct> MapProduct(Product product, DiscountProtoService.DiscountProtoServiceClient discountProto, CancellationToken cancellationToken)
    {
        var response = await discountProto.GetCouponByProductIdAsync(new GetCouponByProductIdRequest { ProductId = product.Id.Value.ToString() }, cancellationToken: cancellationToken);
        var discount = response.Coupon.Adapt<Coupon>();

        return new LiteProduct(
            Id: product.Id.Value,
            Name: product.Name.Value,
            UrlFriendlyName: product.UrlFriendlyName.Value,
            ProductType: product.ProductType,
            ProductTypeString: product.ProductType.ToString(),
            CoverImage: new ImageDto(product.CoverImage.ImageSrc, product.CoverImage.AltText),
            Discount: discount.Code == null ? null : discount,
            ColorVariants: product.ColorVariants.Select(cv => new LiteColorVariant(
                Id: cv.Id.Value,
                Color: cv.Color.Value,
                Background: $"bg-{cv.Color.Value.ToLower()}-500",
                Class: $"text-{cv.Color.Value.ToLower()}-500",
                Image: new ImageDto(cv.Images[0].ImageSrc, cv.Images[0].AltText),
                Price:
                product.ProductType == ProductType.Clothing ? null
                : cv.Price.Amount,
                DiscountedPrice: product.ProductType ==  ProductType.Clothing ? null : CalculateDiscount(discount, cv.Price.Amount.Value),
                InventoryStatus: GetInventoryStatus(product.ProductType, cv).ToString(),
                Slug: cv.Slug.Value,
                SizeVariants: cv.SizeVariants.Select(
                    sv => new LiteSizeVariant(
                        sv.Id.Value, 
                        sv.Size.Value, 
                        sv.Price.Amount,
                        DiscountedPrice: CalculateDiscount(discount, sv.Price.Amount),
                        GetInventoryStatus(sv).ToString())).ToList()
                )).ToList()
        );
    }

    public decimal CalculateDiscount(Coupon coupon, decimal price)
    {
        if (coupon == null) return price;
        if (coupon.FlatAmount > 0) return Math.Max(0, price - coupon.FlatAmount);
        if (coupon.Percentage > 0) return Math.Max(0, price * (1 - (coupon.Percentage / 100m)));
        return price;
    }

    private InventoryStatus? GetInventoryStatus(ProductType productType, ColorVariant cv)
    {
        if (productType != ProductType.Clothing)
        {
            var quantity = cv.Quantity.Value;
            if (quantity > cv.RestockThreshold.Value) return InventoryStatus.INSTOCK;
            else if (quantity < cv.RestockThreshold.Value && quantity > 0) return InventoryStatus.LOWSTOCK;
            return InventoryStatus.OUTOFSTOCK;
        }
        return null;
    }

    private InventoryStatus GetInventoryStatus(SizeVariant sv)
    {
        var quantity = sv.Quantity.Value;
        if (quantity > sv.RestockThreshold.Value) return InventoryStatus.INSTOCK;
        else if (quantity < sv.RestockThreshold.Value && quantity > 0) return InventoryStatus.LOWSTOCK;
        else return InventoryStatus.OUTOFSTOCK;
    }

}
