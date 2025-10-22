using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrJobLevelRepository : GenericRepository<HrJobLevel>, IHrJobLevelRepository
    {
        private readonly ApplicationDbContext _context;

        public HrJobLevelRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
