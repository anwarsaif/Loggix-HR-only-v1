using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrClearanceAllowanceDeductionRepository : GenericRepository<HrClearanceAllowanceDeduction, HrClearanceAllowanceVw>, IHrClearanceAllowanceDeductionRepository
    {
        public HrClearanceAllowanceDeductionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
