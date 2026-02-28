using AutoMapper;
using OMS_Backend.DTOs.Product;
using OMS_Backend.Models;

namespace OMS_Backend.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryIds,
                opt => opt.MapFrom(src => src.Categories.Select(c => c.CategoryId)));

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Categories,
                           opt => opt.Ignore());

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Categories,
                           opt => opt.Ignore());
        }
    }
}