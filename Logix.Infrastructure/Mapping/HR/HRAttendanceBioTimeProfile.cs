using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttendanceBioTimeProfile : Profile
    {
        public HrAttendanceBioTimeProfile()
        {
            CreateMap<HrAttendanceBioTimeDto, HrAttendanceBioTime>().ReverseMap();
        }
    } 
}
