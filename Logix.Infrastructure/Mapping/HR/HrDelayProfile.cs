using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDelayProfile : Profile
    {
        public HrDelayProfile()
        {
            CreateMap<HrDelayDto, HrDelay>().ReverseMap();
            CreateMap<HrDelayEditDto, HrDelay>().ReverseMap();
            CreateMap<HrDelayAddDto, HrDelay>().ReverseMap();
        }
    }
}
