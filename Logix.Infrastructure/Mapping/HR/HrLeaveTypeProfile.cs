using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLeaveTypeProfile : Profile
    {
        public HrLeaveTypeProfile()
        {
            CreateMap<HrLeaveTypeDto, HrLeaveType>().ReverseMap();

        }
    }
}
