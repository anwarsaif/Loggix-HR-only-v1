using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrJobOfferProfile : Profile
    {
        public HrJobOfferProfile()
        {
            CreateMap<HrJobOfferDto, HrJobOffer>().ReverseMap();
            CreateMap<HrJobOfferEditDto, HrJobOffer>().ReverseMap();
        }
    }
}
