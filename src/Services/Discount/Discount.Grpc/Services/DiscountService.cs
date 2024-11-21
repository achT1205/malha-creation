using Discount.Application.Coupons.Commands;
using Discount.Application.Coupons.Queries;
using Grpc.Core;
using Mapster;
using MediatR;

namespace Discount.Grpc.Services
{
    public class DiscountService(ISender sender, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CreateCouponResponse> CreateCoupon(CreateCouponRequest request, ServerCallContext context)
        {
            var command = request.Adapt<CreateCouponCommand>();
            var result = await sender.Send(command);
            logger.LogInformation("Coupon is successfully created. Id : {Id}", result.CouponId);
            var response = result.Adapt<CreateCouponResponse>();
            return response;
        }

        public override async Task<DeleteCouponResponse> DeleteCoupon(DeleteCouponRequest request, ServerCallContext context)
        {
            var command = request.Adapt<DeleteCouponCommand>();
            var result = await sender.Send(command);
            logger.LogInformation("Coupon is successfully deleted. Id : {Id}", command.CouponId);
            var response = result.Adapt<DeleteCouponResponse>();
            return response;
        }

        public override async Task<UpdateCouponResponse> UpdateCoupon(UpdateCouponRequest request, ServerCallContext context)
        {

            var command = request.Adapt<UpdateCouponCommand>();
            var result = await sender.Send(command);
            logger.LogInformation("Coupon is successfully updated. Id : {Id}", command.CouponId);
            var response = result.Adapt<UpdateCouponResponse>();
            return response;
        }

        public override async Task<AddApplicableProductResponse> AddApplicableProduct(AddApplicableProductRequest request, ServerCallContext context)
        {
            var command = request.Adapt<AddApplicableProductCommand>();
            var result = await sender.Send(command);
            logger.LogInformation("Product is successfully added. Id : {Id}", command.ProductId);
            var response = result.Adapt<AddApplicableProductResponse>();
            return response;
        }

        public override async Task<AddCustomerResponse> AddCustomerCoupon(AddCustomerRequest request, ServerCallContext context)
        {
            var command = request.Adapt<AddCustomerCommand>();
            var result = await sender.Send(command);
            logger.LogInformation("Customer is successfully added. Id : {Id}", command.CustomerId);
            var response = result.Adapt<AddCustomerResponse>();
            return response;
        }

        public override async Task<GetCouponForProductResponse> GetCouponForProduct(GetCouponForProductRequest request, ServerCallContext context)
        {
            var command = request.Adapt<GetCouponForProductQuery>();
            var result = await sender.Send(command);
            logger.LogInformation("Coupon is retrieved for ProductId : {ProductId}, CouponCode : {amount}", command.ProductId, result.CouponCode);
            var response = result.Adapt<GetCouponForProductResponse>();
            return response;
        }

        public override async Task<GetBasketDiscountResponse> GetBasketDiscount(GetBasketDiscountRequest request, ServerCallContext context)
        {
            var command = request.Adapt<GetCouponForProductQuery>();
            var result = await sender.Send(command);
            logger.LogInformation("Coupon is retrieved for CouponCode : {amount}", result.CouponCode);
            var response = result.Adapt<GetBasketDiscountResponse>();
            return response;
        }

    }
}
