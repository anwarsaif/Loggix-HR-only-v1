using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPerformanceRepository : GenericRepository<HrPerformance>, IHrPerformanceRepository
    {
        private readonly ApplicationDbContext _context;

        public HrPerformanceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
