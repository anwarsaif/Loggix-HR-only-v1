using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrRecruitmentCandidateKpiDRepository : IGenericRepository<HrRecruitmentCandidateKpiD>
    {
        Task<IEnumerable<HrRecruitmentCandidateKpiDVw>> GetAllVW(Expression<Func<HrRecruitmentCandidateKpiDVw, bool>> expression);

    }

}
