using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrActualAttendanceRepository : GenericRepository<HrActualAttendance>, IHrActualAttendanceRepository
    {
        private readonly ApplicationDbContext context;

        public HrActualAttendanceRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrActualAttendanceVw>> GetAllFromView(Expression<Func<HrActualAttendanceVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrActualAttendanceVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrActualAttendanceVw>();
            }
        }

    }
}
