using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrFlexibleWorkingRepository : IGenericRepository<HrFlexibleWorking>
    {
        Task<IEnumerable<HrFlexibleWorkingVw>> GetAllFromView(Expression<Func<HrFlexibleWorkingVw, bool>> expression);

    }

}
