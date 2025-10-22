using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrKpiTemplatesCompetenceProfile : Profile
    {
        public HrKpiTemplatesCompetenceProfile()
        {
            CreateMap<HrKpiTemplatesCompetenceDto, HrKpiTemplatesCompetence>().ReverseMap();
            CreateMap<HrKpiTemplatesCompetenceEditDto, HrKpiTemplatesCompetence>().ReverseMap();
        }
    }
}
