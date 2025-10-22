using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrRecruitmentCandidateKpiRepository : IGenericRepository<HrRecruitmentCandidateKpi>
    {
        Task<IEnumerable<HrRecruitmentCandidateKpiVw>> GetAllVW(Expression<Func<HrRecruitmentCandidateKpiVw, bool>> expression);

    }

}
