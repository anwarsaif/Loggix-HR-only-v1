using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLanguageProfile : Profile
    {
        public HrLanguageProfile()
        {
            CreateMap<HrLanguageDto, HrLanguage>().ReverseMap();
            CreateMap<HrLanguageEditDto, HrLanguage>().ReverseMap();
        }
    }
}
