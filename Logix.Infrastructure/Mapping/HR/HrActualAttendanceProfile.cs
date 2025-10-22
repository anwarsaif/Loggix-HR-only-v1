using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrActualAttendanceProfile : Profile
    {
        public HrActualAttendanceProfile()
        {
            CreateMap<HrActualAttendanceDto, HrActualAttendance>().ReverseMap();
            CreateMap<HrActualAttendanceReportDto, HrActualAttendanceVw>().ReverseMap();
        }
    }
}
