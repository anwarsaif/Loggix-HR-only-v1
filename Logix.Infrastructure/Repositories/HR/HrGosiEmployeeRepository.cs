using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrGosiEmployeeRepository : GenericRepository<HrGosiEmployee>, IHrGosiEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public HrGosiEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrGosiEmployeeVw>> GetAllFromView(Expression<Func<HrGosiEmployeeVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrGosiEmployeeVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrGosiEmployeeVw>();
            }
        }
    }
}
