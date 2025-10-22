using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPayrollDeductionVwRepository : GenericRepository<HrPayrollDeductionVw>, IHrPayrollDeductionVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrPayrollDeductionVwRepository(ApplicationDbContext context, ICurrentData session) : base(context)
        {
            this._context = context;
        }

    }



}
