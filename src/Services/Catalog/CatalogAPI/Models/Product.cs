namespace CatalogAPI.Models;

public class Product
{
    public Guid Id { get; set; }
    // Since C# 8, nullable reference types exist. I know this will be null right now, but trust me, I will assign a non-null value before using it.
    public string Name { get; set; } = default!;
    // new() removes the need to separately initialize the property before use
    public List<string> Category { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}
