using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmpGoalIndicatorProfile : Profile
    {
        public HrEmpGoalIndicatorProfile()
        {
            CreateMap<HrEmpGoalIndicatorDto, HrEmpGoalIndicator>().ReverseMap();
            CreateMap<HrEmpGoalIndicatorEditDto, HrEmpGoalIndicator>().ReverseMap();
        }
    }
}
