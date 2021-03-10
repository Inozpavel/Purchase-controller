using AutoMapper;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Mapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoreRequest, Store>();
        }
    }
}