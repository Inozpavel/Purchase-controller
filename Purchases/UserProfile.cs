﻿using AutoMapper;
using Purchases.Entities;
using Purchases.Models;

namespace Purchases
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Password, opt => opt.Ignore())
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Patronymic));
        }
    }
}