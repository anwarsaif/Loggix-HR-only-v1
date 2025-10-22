using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrPsAllowanceDeductionRepository : IGenericRepository<HrPsAllowanceDeduction>
    {
        Task<IEnumerable<HrPsAllowanceDeductionVw>> GetAllFromView(Expression<Func<HrPsAllowanceDeductionVw, bool>> expression);

    }
}
