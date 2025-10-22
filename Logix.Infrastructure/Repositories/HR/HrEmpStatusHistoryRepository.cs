using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrEmpStatusHistoryRepository : GenericRepository<HrEmpStatusHistory, HrEmpStatusHistoryVw>, IHrEmpStatusHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public HrEmpStatusHistoryRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
