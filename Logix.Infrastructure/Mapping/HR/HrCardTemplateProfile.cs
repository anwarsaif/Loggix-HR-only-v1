using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCardTemplateProfile : Profile
    {
        public HrCardTemplateProfile()
        {
            CreateMap<HrCardTemplateDto, HrCardTemplate>().ReverseMap();
            CreateMap<HrCardTemplateEditDto, HrCardTemplate>().ReverseMap();
        }
    }
}
