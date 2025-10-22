using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrAllowanceDeductionRepository : IGenericRepository<HrAllowanceDeduction, HrAllowanceDeductionVw>
    {
        Task<IEnumerable<HrAllowanceDeductionVw>> GetAllFromView(Expression<Func<HrAllowanceDeductionVw, bool>> expression);
        Task<decimal> GetTotalAllowances(long EmpId);
        Task<decimal> GetTotalDeduction(long EmpId);
        Task<int> Chk_Allowance_Deduction_Exists_2(long empId, int typeId, int adId, int fixedOrTemporary, string dueDate);


    }
}

