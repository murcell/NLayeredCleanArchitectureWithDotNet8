using App.Application.Feature.Products.Dto;

namespace App.Application.Feature.Category.Dto;

public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);

