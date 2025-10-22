using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRecruitmentCandidateService : IGenericQueryService<HrRecruitmentCandidateDto, HrRecruitmentCandidateVw>, IGenericWriteService<HrRecruitmentCandidateDto, HrRecruitmentCandidateEditDto>
    {
        Task<IResult<HrRecruitmentCandidateDto>> RecruitmentCandidateAdd(HrRecruitmentCandidateDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrRecruitmentCandidateEditDto>> RecruitmentCandidateEdit(HrRecruitmentCandidateEditDto entity, CancellationToken cancellationToken = default);
		Task<IResult<List<HrRecruitmentCandidateFilterDto>>> Search(HrRecruitmentCandidateFilterDto filter, CancellationToken cancellationToken = default);


	}


}
