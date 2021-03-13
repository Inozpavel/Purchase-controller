using AutoMapper;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());
        }
    }
}