using AutoMapper;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Mapper
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<PurchaseRequest, Purchase>()
                .ForMember(dest => dest.ReceiptPositions, opt => opt.Ignore());
        }
    }
}