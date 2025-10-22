using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrOhadDetailProfile : Profile
    {
        public HrOhadDetailProfile()
        {
            CreateMap<HrOhadDetailDto, HrOhadDetail>().ReverseMap();
            CreateMap<HrOhadDetailEditDto, HrOhadDetail>().ReverseMap();
        }
    }
}
