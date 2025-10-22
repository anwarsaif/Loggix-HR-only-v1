using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrSkillProfile : Profile
    {
        public HrSkillProfile()
        {
            CreateMap<HrSkillDto, HrSkill>().ReverseMap();
            CreateMap<HrSkillEditDto, HrSkill>().ReverseMap();
        }
    }
}
