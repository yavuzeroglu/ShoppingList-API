using AutoMapper;
using ShoppingList.Application.DTOs.Categories;
using ShoppingList.Application.DTOs.Products;
using ShoppingList.Application.Features.Categories.Commands.CreateCategory;
using ShoppingList.Application.Features.Products.Commands.UpdateProduct;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mapping;


public class MappingProfile : Profile
{
   public MappingProfile()
   {

      CreateMap<CreateProductDTO, Product>().ReverseMap();
      CreateMap<UpdateProductCommandRequest, Product>();
      CreateMap<Product, ListProductDTO>()
         .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
         .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));




      CreateMap<Category, ListCategoryDTO>()
         .ForMember(dest => dest.ParentCategoryName,
                   opt => opt.MapFrom(src => src.ParentCategory.Name))
         .ForMember(dest => dest.SubCategories,
                    opt => opt.MapFrom(src => src.SubCategories.Select(sc => sc.Name)))
         .ForMember(dest => dest.Products,
                  opt => opt.MapFrom(src => src.Products.Select(p => p.Name))).ReverseMap();


      CreateMap<CreateCategoryCommandRequest, Category>();
   }
}

