using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttendanceRepository : GenericRepository<HrAttendance>, IHrAttendanceRepository
    {
        private readonly ApplicationDbContext context;

        public HrAttendanceRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrAttendancesVw>> GetAllFromView(Expression<Func<HrAttendancesVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrAttendancesVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrAttendancesVw>();
            }
        }

    }



}
