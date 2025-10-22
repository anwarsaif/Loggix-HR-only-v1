using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRecruitmentCandidateProfile : Profile
    {
        public HrRecruitmentCandidateProfile()
        {
            CreateMap<HrRecruitmentCandidateDto, HrRecruitmentCandidate>().ReverseMap();
            CreateMap<HrRecruitmentCandidateEditDto, HrRecruitmentCandidate>().ReverseMap();
        }
    }
}
