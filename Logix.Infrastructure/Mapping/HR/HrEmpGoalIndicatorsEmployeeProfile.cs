using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmpGoalIndicatorsEmployeeProfile : Profile
    {
        public HrEmpGoalIndicatorsEmployeeProfile()
        {
            CreateMap<HrEmpGoalIndicatorsEmployeeDto, HrEmpGoalIndicatorsEmployee>().ReverseMap();
            CreateMap<HrEmpGoalIndicatorsEmployeeEditDto, HrEmpGoalIndicatorsEmployee>().ReverseMap();
        }
    }
}
