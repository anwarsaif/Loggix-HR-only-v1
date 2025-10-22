using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRecruitmentVacancyProfile : Profile
    {
        public HrRecruitmentVacancyProfile()
        {
            CreateMap<HrRecruitmentVacancyDto, HrRecruitmentVacancy>().ReverseMap();
            CreateMap<HrRecruitmentVacancyEditDto, HrRecruitmentVacancy>().ReverseMap();
            CreateMap<HrRecruitmentVacancyVwDto, HrRecruitmentVacancyVw>().ReverseMap();
        }
    }
}
