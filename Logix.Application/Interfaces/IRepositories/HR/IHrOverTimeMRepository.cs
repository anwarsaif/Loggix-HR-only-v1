using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrOverTimeMRepository : IGenericRepository<HrOverTimeM, HrOverTimeMVw>
    {
        Task<IEnumerable<HrOverTimeMVw>> GetAllFromView(Expression<Func<HrOverTimeMVw, bool>> expression);

    }

}
