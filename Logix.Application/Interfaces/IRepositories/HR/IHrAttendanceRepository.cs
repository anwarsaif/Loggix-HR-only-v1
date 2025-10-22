using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrAttendanceRepository : IGenericRepository<HrAttendance>
    {
        Task<IEnumerable<HrAttendancesVw>> GetAllFromView(Expression<Func<HrAttendancesVw, bool>> expression);

    }

}
