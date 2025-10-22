using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrIncrementsAllowanceDeductionRepository : GenericRepository<HrIncrementsAllowanceDeduction>, IHrIncrementsAllowanceDeductionRepository
    {
        private readonly ApplicationDbContext _context;

        public HrIncrementsAllowanceDeductionRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
