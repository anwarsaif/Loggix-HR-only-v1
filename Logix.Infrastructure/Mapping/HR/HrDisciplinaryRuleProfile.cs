using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDisciplinaryRuleProfile : Profile
    {
        public HrDisciplinaryRuleProfile()
        {
            CreateMap<HrDisciplinaryRuleDto, HrDisciplinaryRule>().ReverseMap();
            CreateMap<HrDisciplinaryRuleEditDto, HrDisciplinaryRule>().ReverseMap();
        }
    }
}
