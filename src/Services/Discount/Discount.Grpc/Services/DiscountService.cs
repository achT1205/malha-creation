using Discount.Application.Coupons.Commands;
using Discount.Application.Coupons.Queries;
using Grpc.Core;
using Mapster;
using MediatR;
using System;

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
            var command = request.Adapt<GetBasketDiscountQuery>();
            var result = await sender.Send(command);
            logger.LogInformation("Coupon is retrieved for CouponCode : {code}", result.CouponCode);
            var response = result.Adapt<GetBasketDiscountResponse>();
            return response;
        }

        public override async Task<Coupon> GetCoupon(GetCouponRequest request, ServerCallContext context)
        {
            try
            {
                var command = new GetCouponQuery(new Guid(request.Id));
                var result = await sender.Send(command);
                logger.LogInformation("Coupon is retrieved for Id : {id}", command.Id);
                return MapCoupon(result.Coupon);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override async Task<GetCouponsResponse> GetCoupons(GetCouponsRequest request, ServerCallContext context)
        {
            var command = new GetCouponsQuery();
            var result = await sender.Send(command);
            logger.LogInformation("Coupons are retrieved");
            var response = new GetCouponsResponse();
            if (result.Coupons == null)
                return response;

            foreach (var coupon in result.Coupons)
            {
                if (coupon != null)
                {
                    Coupon c = MapCoupon(coupon);
                    response.Coupons.Add(c);
                }
            }
            return response;
        }

        public override async Task<GetCouponByProductIdResponse> GetCouponByProductId(GetCouponByProductIdRequest request, ServerCallContext context)
        {
            try
            {
                var command = new GetCouponByProductIdQuery(new Guid(request.ProductId));
                var result = await sender.Send(command);
                logger.LogInformation("Coupon is retrieved for ProductId : {ProductId}, CouponCode : {amount}", command.ProductId, result.Coupon?.Code);

                LiteCoupon coupon = new LiteCoupon();
                if (result.Coupon != null)
                {
                    coupon.Id = result.Coupon.Id.ToString();
                    coupon.Code = result.Coupon.Code.Value;
                    coupon.Name = result.Coupon.Name;
                    coupon.Description = result.Coupon.Description;
                    coupon.FlatAmount = result.Coupon.Discountable.FlatAmount.HasValue ?  (double)result.Coupon.Discountable.FlatAmount.Value : 0;
                    coupon.Percentage = result.Coupon.Discountable.Percentage.HasValue ? result.Coupon.Discountable.Percentage.Value : 0;
                }

                return new GetCouponByProductIdResponse() { Coupon = coupon };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static Coupon MapCoupon(Domain.Models.Coupon coupon)
        {
            var c = new Coupon
            {
                Id = coupon.Id.Value.ToString(),
                Code = coupon.Code.Value,
                Name = coupon.Name,
                Description = coupon.Description,
                FlatAmount = coupon.Discountable.FlatAmount != null ? (double)coupon.Discountable.FlatAmount.Value : 0,
                Percentage = coupon.Discountable.Percentage != null ? coupon.Discountable.Percentage.Value : 0,
                StartDate = coupon.StartDate.ToString(),
                EndDate = coupon.EndDate.ToString(),
                MaxUses = coupon?.MaxUses ?? 0,
                TotalRedemptions = coupon?.TotalRedemptions ?? 0,
                MaxUsesPerCustomer = coupon?.MaxUsesPerCustomer ?? 0,
                MinimumOrderValue = coupon?.MinimumOrderValue != null ? (double)coupon?.MinimumOrderValue.Value : 0,
                IsFirstTimeOrderOnly = coupon?.IsFirstTimeOrderOnly ?? false,
                IsActive = coupon?.IsActive ?? false,
                IsDeleted = coupon?.IsDeleted ?? false,
            };
            c.ProductIds.AddRange(coupon.ProductIds.Select(id => id.Value.ToString()));
            c.AllowedCustomerIds.AddRange(coupon.AllowedCustomerIds.Select(id => id.Value.ToString()));
            return c;
        }
    }
}
