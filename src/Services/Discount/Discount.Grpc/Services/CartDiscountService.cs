using CartDiscount.Grpc;
using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;


public class CartDiscountService(DiscountContext dbContext, ILogger<CartDiscountService> logger) : CartDiscountProtoService.CartDiscountProtoServiceBase
{
    public override async Task<CartCouponModel> GetCartDiscount(GetCartDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.CartCoupons.FirstOrDefaultAsync(_ => _.CouponCode == request.CouponCode);
        if (coupon == null)
            coupon = new CartCoupon { CouponCode = "No Discount", DiscountRate = 0, Description = "" };
        logger.LogInformation("Cart Discount is retrieved : {code}, rate : {amount}", coupon.CouponCode, coupon.DiscountRate);
        var couponModel = coupon.Adapt<CartCouponModel>();
        return couponModel;
    }

    public override async Task<CartCouponModel> CreateCartDiscount(CreateCartDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<CartCoupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.CartCoupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. CouponCode : {CouponCode}", coupon.CouponCode);

        var couponModel = coupon.Adapt<CartCouponModel>();
        return couponModel;
    }

    public override async Task<CartCouponModel> UpdateCartDiscount(UpdateCartDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<CartCoupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.CartCoupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. CouponCode : {CouponCode}", coupon.CouponCode);

        var couponModel = coupon.Adapt<CartCouponModel>();
        return couponModel;
    }

    public override async Task<DeleteCartDiscountResponse> DeleteCartDiscount(DeleteCartDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
             .Coupons
             .FirstOrDefaultAsync(x => x.ProductId == request.CouponCode);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with CouponCode={request.CouponCode} is not found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. CouponCode : {CouponCode}", request.CouponCode);

        return new DeleteCartDiscountResponse { Success = true };
    }
}