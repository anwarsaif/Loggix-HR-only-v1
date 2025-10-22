using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrExpensesEmployeeRepository : GenericRepository<HrExpensesEmployee>, IHrExpensesEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public HrExpensesEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrExpensesEmployeesVw>> GetAllFromView(Expression<Func<HrExpensesEmployeesVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrExpensesEmployeesVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrExpensesEmployeesVw>();
            }
        }
    }
}
