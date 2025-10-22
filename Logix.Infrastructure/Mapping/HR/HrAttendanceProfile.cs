using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttendanceProfile : Profile
    {
        public HrAttendanceProfile()
        {
            CreateMap<HrAttendanceDto, HrAttendance>().ReverseMap();
            CreateMap<HrAttendanceEditDto, HrAttendance>().ReverseMap();
        }
    } 
}
