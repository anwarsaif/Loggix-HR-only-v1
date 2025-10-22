using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Logix.Infrastructure.EntityConfigurations.HR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPsAllowanceDeductionRepository : GenericRepository<HrPsAllowanceDeduction>, IHrPsAllowanceDeductionRepository
    {
        private readonly ApplicationDbContext context;

        public HrPsAllowanceDeductionRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrPsAllowanceDeductionVw>> GetAllFromView(Expression<Func<HrPsAllowanceDeductionVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrPsAllowanceDeductionVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrPsAllowanceDeductionVw>();
            }
        }

    }

}
