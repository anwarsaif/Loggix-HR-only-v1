using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrWorkExperienceProfile : Profile
    {
        public HrWorkExperienceProfile()
        {
            CreateMap<HrWorkExperienceDto, HrWorkExperience>().ReverseMap();
            CreateMap<HrWorkExperienceEditDto, HrWorkExperience>().ReverseMap();
        }
    }
}
