using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrGosiEmployeeRepository : IGenericRepository<HrGosiEmployee>
    {
        Task<IEnumerable<HrGosiEmployeeVw>> GetAllFromView(Expression<Func<HrGosiEmployeeVw, bool>> expression);

    }

}
