namespace App.Application.Feature.Products.Dto;

//public record ProductDto
//{
//	public int Id { get; init; }
//	public string Name { get; init; } = default!;
//	public decimal Price { get; init; }
//	public int Stock { get; init; }
//}

//primary contructor
public record ProductDto(int Id, string Name, decimal Price, int Stock, int CategoryId);

