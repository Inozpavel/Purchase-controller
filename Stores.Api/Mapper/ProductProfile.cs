using AutoMapper;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Mapper
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