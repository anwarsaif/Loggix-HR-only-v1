using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrOverTimeMRepository : GenericRepository<HrOverTimeM, HrOverTimeMVw>, IHrOverTimeMRepository
    {
        private readonly ApplicationDbContext context;

        public HrOverTimeMRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrOverTimeMVw>> GetAllFromView(Expression<Func<HrOverTimeMVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrOverTimeMVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrOverTimeMVw>();
            }
        }

    }



}
