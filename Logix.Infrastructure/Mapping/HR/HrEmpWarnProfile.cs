using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmpWarnProfile : Profile
    {
        public HrEmpWarnProfile()
        {
            CreateMap<HrEmpWarnDto, HrEmpWarn>().ReverseMap();
            CreateMap<HrEmpWarnEditDto, HrEmpWarn>().ReverseMap();
        }
    }
}
