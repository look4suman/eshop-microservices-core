using BuildingBlocks.CQRS;
using CatalogAPI.Models;

namespace CatalogAPI.Products.CreateProduct;

// record - reference type that behaves like a value type. Different from struct. Immutable.
// var p1 = new Point(1, 2);
// var p2 = p1 with { X = 10 };

// Console.WriteLine(p1.X); // 1
// Console.WriteLine(p2.X); // 10
// C# record ≈ Go *struct + immutability + value equality

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // create product entity from command object
        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // save to db
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // return CreateProductResult
        return new CreateProductResult(product.Id);
    }
}
