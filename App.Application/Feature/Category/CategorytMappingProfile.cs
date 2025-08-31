using App.Application.Feature.Category.Create;
using App.Application.Feature.Category.Dto;
using App.Application.Feature.Category.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Services.Categories;

public class CategorytMappingProfile : Profile
{
	public CategorytMappingProfile()
	{
		CreateMap<Category, CategoryDto>().ReverseMap();

		CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
		CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

		CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
	}
}
