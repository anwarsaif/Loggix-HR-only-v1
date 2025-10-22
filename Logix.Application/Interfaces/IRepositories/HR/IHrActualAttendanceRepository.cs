using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrActualAttendanceRepository : IGenericRepository<HrActualAttendance>
    {
        Task<IEnumerable<HrActualAttendanceVw>> GetAllFromView(Expression<Func<HrActualAttendanceVw, bool>> expression);

    }

}
