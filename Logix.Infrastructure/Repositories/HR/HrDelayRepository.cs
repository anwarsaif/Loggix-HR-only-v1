using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDelayRepository : GenericRepository<HrDelay, HrDelayVw>, IHrDelayRepository
    {
        private readonly ApplicationDbContext context;

        public HrDelayRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrDelayVw>> GetAllFromView(Expression<Func<HrDelayVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrDelayVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrDelayVw>();
            }
        }

    }
}
