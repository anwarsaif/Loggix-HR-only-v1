using Logix.Domain.HR;
using Logix.Domain.PM;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrRecruitmentVacancyRepository : IGenericRepository<HrRecruitmentVacancy>
    {
        Task<IEnumerable<HrRecruitmentVacancyVw>> GetAllVW(Expression<Func<HrRecruitmentVacancyVw, bool>> expression);

    }
}
