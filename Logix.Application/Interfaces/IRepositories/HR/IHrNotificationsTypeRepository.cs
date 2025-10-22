using Logix.Application.Common;
using Logix.Domain.HR;
using System.Linq.Expressions;


namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrNotificationsTypeRepository : IGenericRepository<HrNotificationsType>
    {
        Task<IEnumerable<HrNotificationsTypeVw>> GetAllVW(Expression<Func<HrNotificationsTypeVw, bool>> expression);

    }

}
