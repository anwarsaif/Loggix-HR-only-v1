using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttTimeTableDayProfile : Profile
    {
        public HrAttTimeTableDayProfile()
        {
            CreateMap<HrAttTimeTableDayDto, HrAttTimeTableDay>().ReverseMap();
            CreateMap<HrAttTimeTableDayEditDto, HrAttTimeTableDay>().ReverseMap();
        }
    }
}
