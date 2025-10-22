using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrFlexibleWorkingRepository : GenericRepository<HrFlexibleWorking>, IHrFlexibleWorkingRepository
    {
        private readonly ApplicationDbContext context;

        public HrFlexibleWorkingRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrFlexibleWorkingVw>> GetAllFromView(Expression<Func<HrFlexibleWorkingVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrFlexibleWorkingVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrFlexibleWorkingVw>();
            }
        }

    }
}
