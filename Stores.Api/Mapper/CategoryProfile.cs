using AutoMapper;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequest, StoreCategory>().ForMember(dst => dst.StoreCategoryName,
                opt => opt.MapFrom(src => src.CategoryName));
        }
    }
}