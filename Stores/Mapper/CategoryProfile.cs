using AutoMapper;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Mapper
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