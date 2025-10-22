using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRecruitmentCandidateKpiService : IGenericQueryService<HrRecruitmentCandidateKpiDto, HrRecruitmentCandidateKpiVw>, IGenericWriteService<HrRecruitmentCandidateKpiDto, HrRecruitmentCandidateKpiEditDto>
    {
        Task<IResult<IEnumerable<HrRecruitmentCandidateKpiFilterDto>>> SearchHrRecruitmentCandidateKp(HrRecruitmentCandidateKpiFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<string>> AddNewCandidateKpi(HrCandidateKPIDtoForOperations entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> UpdateCandidateKpi(HrCandidateKPIDtoForOperations entity, CancellationToken cancellationToken = default);

    }


}
