using AutoMapper;
using Purchases.DTOs;
using Purchases.Entities;

namespace Purchases.Mapper
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