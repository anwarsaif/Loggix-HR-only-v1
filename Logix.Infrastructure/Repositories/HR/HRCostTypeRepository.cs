using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.ACC;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCostTypeRepository : GenericRepository<HrCostType>, IHrCostTypeRepository
    {
        private readonly ApplicationDbContext context;

        public HrCostTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrCostTypeVw>> GetAllFromView(Expression<Func<HrCostTypeVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrCostTypeVw>().Where(expression).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return new List<HrCostTypeVw>();
            }
        }


    }


}
