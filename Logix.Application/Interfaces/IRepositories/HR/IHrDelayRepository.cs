using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrDelayRepository : IGenericRepository<HrDelay, HrDelayVw>
    {
        Task<IEnumerable<HrDelayVw>> GetAllFromView(Expression<Func<HrDelayVw, bool>> expression);

    }
}
