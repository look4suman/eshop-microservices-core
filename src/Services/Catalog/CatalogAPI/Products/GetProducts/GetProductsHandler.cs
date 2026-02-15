using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using Discount.Grpc;

namespace CatalogAPI.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger, DiscountProtoService.DiscountProtoServiceClient serviceClient)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.handle method is called with {@query}", query);

        var request = new GetDiscountRequest { ProductName = "IPhone X" };
        var couponModel = await serviceClient.GetDiscountAsync(request);

        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
