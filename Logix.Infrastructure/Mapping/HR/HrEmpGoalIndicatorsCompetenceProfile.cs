using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmpGoalIndicatorsCompetenceProfile : Profile
    {
        public HrEmpGoalIndicatorsCompetenceProfile()
        {
            CreateMap<HrEmpGoalIndicatorsCompetenceDto, HrEmpGoalIndicatorsCompetence>().ReverseMap();
            CreateMap<HrEmpGoalIndicatorsCompetenceEditDto, HrEmpGoalIndicatorsCompetence>().ReverseMap();
        }
    }
}
