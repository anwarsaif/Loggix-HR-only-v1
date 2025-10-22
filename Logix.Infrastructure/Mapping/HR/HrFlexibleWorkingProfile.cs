using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrFlexibleWorkingProfile : Profile
    {
        public HrFlexibleWorkingProfile()
        {
            CreateMap<HrFlexibleWorkingDto, HrFlexibleWorking>().ReverseMap();
            CreateMap<HrFlexibleWorkingEditDto, HrFlexibleWorking>().ReverseMap();
        }
    }
}
