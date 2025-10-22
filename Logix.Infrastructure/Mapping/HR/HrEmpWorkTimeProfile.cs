using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmpWorkTimeProfile : Profile
    {
        public HrEmpWorkTimeProfile()
        {
            CreateMap<HrEmpWorkTimeDto, HrEmpWorkTime>().ReverseMap();
            CreateMap<HrEmpWorkTimeEditDto, HrEmpWorkTime>().ReverseMap();
        }
    }
}
