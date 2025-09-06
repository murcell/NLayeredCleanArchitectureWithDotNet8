using App.Application.Feature.Products.Create;
using App.Application.Feature.Products.Dto;
using App.Application.Feature.Products.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Services.Products;

public class ProductMappingProfile:Profile
{
	public ProductMappingProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap();
		CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
		CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
	}
}
