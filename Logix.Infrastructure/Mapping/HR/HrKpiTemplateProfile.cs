using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrKpiTemplateProfile : Profile
    {
        public HrKpiTemplateProfile()
        {
            CreateMap<HrKpiTemplateDto, HrKpiTemplate>().ReverseMap();
            CreateMap<HrKpiTemplateEditDto, HrKpiTemplate>().ReverseMap();
        }
    }
}
