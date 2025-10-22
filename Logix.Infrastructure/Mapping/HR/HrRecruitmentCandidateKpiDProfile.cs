using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRecruitmentCandidateKpiDProfile : Profile
    {
        public HrRecruitmentCandidateKpiDProfile()
        {
            CreateMap<HrRecruitmentCandidateKpiDDto, HrRecruitmentCandidateKpiD>().ReverseMap();
            CreateMap<HrRecruitmentCandidateKpiDEditDto, HrRecruitmentCandidateKpiD>().ReverseMap();
        }
    }
}
