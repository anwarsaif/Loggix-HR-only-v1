using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRecruitmentCandidateKpiProfile : Profile
    {
        public HrRecruitmentCandidateKpiProfile()
        {
            CreateMap<HrRecruitmentCandidateKpiDto, HrRecruitmentCandidateKpi>().ReverseMap();
            CreateMap<HrRecruitmentCandidateKpiEditDto, HrRecruitmentCandidateKpi>().ReverseMap();
        }
    }
}
