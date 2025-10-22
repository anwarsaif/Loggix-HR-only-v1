using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrJobOfferAdvantageProfile : Profile
    {
        public HrJobOfferAdvantageProfile()
        {
            CreateMap<HrJobOfferAdvantageDto, HrJobOfferAdvantage>().ReverseMap();
            CreateMap<HrJobOfferAdvantageEditDto, HrJobOfferAdvantage>().ReverseMap();
        }
    }
}
