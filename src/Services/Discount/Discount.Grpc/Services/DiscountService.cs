using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(statusCode:StatusCode.InvalidArgument, "Invalid Request"));
        }

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Discount is successfully created. ProductName = {coupon.ProductName}");

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        // Keep method asynchronous to match signature and allow cancellation later
        await Task.CompletedTask;

        // Original DB call (commented out for local testing with hardcoded data)
        // var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName, context.CancellationToken);

        // Hardcoded dummy coupons for testing
        var coupons = new List<Coupon>
        {
            new Coupon { Id = 1, ProductName = "IPhone X", Description = "Discount 10%", Amount = 150m },
            new Coupon { Id = 2, ProductName = "Samsung Galaxy", Description = "Discount 15%", Amount = 200m },
            new Coupon { Id = 3, ProductName = "Pixel 6", Description = "Discount 5%", Amount = 50m }
        };

        var coupon = coupons.FirstOrDefault(x => x.ProductName == request.ProductName);

        //var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
        {
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
        }
        ;

        logger.LogInformation($"ProductName = {coupon.ProductName}, Amount = {coupon.Amount}");

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }
}
