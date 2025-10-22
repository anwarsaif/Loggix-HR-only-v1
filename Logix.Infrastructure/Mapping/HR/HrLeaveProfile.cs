using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLeaveProfile : Profile
    {
        public HrLeaveProfile()
        {
            CreateMap<HrLeaveDto, HrLeave>().ReverseMap();
            CreateMap<HrLeaveEditDto, HrLeave>().ReverseMap();
            CreateMap<HrLeaveAddDto, HrLeave>().ReverseMap();
            CreateMap<HrLeaveGetByIdDto, HrLeaveVw>().ReverseMap();
        }
    }
}
