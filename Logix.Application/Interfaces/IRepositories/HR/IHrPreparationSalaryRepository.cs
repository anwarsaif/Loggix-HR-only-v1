using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrPreparationSalaryRepository : IGenericRepository<HrPreparationSalary>
    {
        Task<IEnumerable<HrPreparationSalariesVw>> GetAllFromView(Expression<Func<HrPreparationSalariesVw, bool>> expression);
        Task<List<HrPayrollCompareResult>> PreparationSalariesPayrollCompare(HrPayrollCompareFilterDto filter, int CmdType);
    }
}
