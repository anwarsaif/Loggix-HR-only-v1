using Logix.Domain.ACC;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrCostTypeRepository : IGenericRepository<HrCostType>
    {
        Task<IEnumerable<HrCostTypeVw>> GetAllFromView(Expression<Func<HrCostTypeVw, bool>> expression);

    }
}
