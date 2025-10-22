using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRecruitmentCandidateKpiDService : IGenericQueryService<HrRecruitmentCandidateKpiDDto, HrRecruitmentCandidateKpiDVw>, IGenericWriteService<HrRecruitmentCandidateKpiDDto, HrRecruitmentCandidateKpiDEditDto>
    {

    }


}
