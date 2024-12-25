using AutoMapper;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Application.DTOs.Categories;
using ShoppingList.Application.DTOs.Products;
using ShoppingList.Application.Features.Categories.Commands.CreateCategory;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;
using ShoppingList.Application.Features.Products.Commands.UpdateProduct;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Mapping;

public class MappingProfile : Profile
{
   public MappingProfile()
   {

      // Product Mapping
      CreateMap<CreateProductCommandRequest, Product>().ReverseMap();
      CreateMap<UpdateProductCommandRequest, Product>();
      CreateMap<Product, ListProductDTO>()
         .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
         .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));


      // Category Mapping
      CreateMap<Category, ListCategoryDTO>()
         .ForMember(dest => dest.ParentCategoryName,
                   opt => opt.MapFrom(src => src.ParentCategory.Name))
         .ForMember(dest => dest.SubCategories,
                    opt => opt.MapFrom(src => src.SubCategories.Select(sc => sc.Name)))
         .ForMember(dest => dest.Products,
                  opt => opt.MapFrom(src => src.Products.Select(p => p.Name)))
         .ReverseMap();
      CreateMap<CreateCategoryCommandRequest, Category>();

      // Basket Mapping
      CreateMap<Basket, BasketViewModel>()
         .ForMember(dest => dest.CreatedByUser,
            opt => opt.MapFrom(src => src.CreatedByUser.UserName))
         .ForMember(dest => dest.Items,
            opt => opt.MapFrom(src => src.BasketItems))
         .ReverseMap();
      CreateMap<BasketItem, BasketItemViewModel>()
         .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
      .ReverseMap();
   }
}

