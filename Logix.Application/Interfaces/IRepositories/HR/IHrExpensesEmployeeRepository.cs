using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrExpensesEmployeeRepository : IGenericRepository<HrExpensesEmployee>
    {
        Task<IEnumerable<HrExpensesEmployeesVw>> GetAllFromView(Expression<Func<HrExpensesEmployeesVw, bool>> expression);

    }

}
