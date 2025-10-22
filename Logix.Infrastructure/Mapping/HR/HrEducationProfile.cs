using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEducationProfile : Profile
    {
        public HrEducationProfile()
        {
            CreateMap<HrEducationDto, HrEducation>().ReverseMap();
            CreateMap<HrEducationEditDto, HrEducation>().ReverseMap();
        }
    }
}
