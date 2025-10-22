using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrVisitScheduleLocationProfile : Profile
    {
        public HrVisitScheduleLocationProfile()
        {
            CreateMap<HrVisitScheduleLocationDto, HrVisitScheduleLocation>().ReverseMap();
            CreateMap<HrVisitScheduleLocationEditDto, HrVisitScheduleLocation>().ReverseMap();
        }
    }
}
