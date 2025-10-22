using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrSalaryGroupRefranceRepository : GenericRepository<HrSalaryGroupRefrance>, IHrSalaryGroupRefranceRepository
    {
        private readonly ApplicationDbContext _context;

        public HrSalaryGroupRefranceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
