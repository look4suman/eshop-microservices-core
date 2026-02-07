using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using MediatR;

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

internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // create product entity from command object
        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile
        };

        // save to db


        // return CreateProductResult
        return new CreateProductResult(Guid.NewGuid());
    }
}
