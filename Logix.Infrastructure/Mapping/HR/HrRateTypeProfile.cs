using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRateTypeProfile : Profile
    {
        public HrRateTypeProfile()
        {
            CreateMap<HrRateTypeDto, HrRateType>().ReverseMap();
            CreateMap<HrRateTypeEditDto, HrRateType>().ReverseMap();
        }
    }
}
