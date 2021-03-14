using AutoMapper;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Mapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoreRequest, Store>();
        }
    }
}