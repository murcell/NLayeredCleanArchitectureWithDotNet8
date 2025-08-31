namespace App.Application.Feature.Products.Update;

public record UpdateProductRequest(string Name, decimal Price, int Stock, int CategoryId);
