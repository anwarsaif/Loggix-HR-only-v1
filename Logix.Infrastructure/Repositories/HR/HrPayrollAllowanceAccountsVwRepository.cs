using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPayrollAllowanceAccountsVwRepository : GenericRepository<HrPayrollAllowanceAccountsVw>, IHrPayrollAllowanceAccountsVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrPayrollAllowanceAccountsVwRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
       
    }
}
