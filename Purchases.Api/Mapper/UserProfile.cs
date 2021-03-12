using AutoMapper;
using Purchases.Api.DTOs;
using Purchases.Api.Entities;

namespace Purchases.Api.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(dst => dst.Password, opt => opt.Ignore());
        }
    }
}