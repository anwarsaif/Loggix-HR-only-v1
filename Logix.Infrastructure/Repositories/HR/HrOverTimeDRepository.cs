using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrOverTimeDRepository : GenericRepository<HrOverTimeD>, IHrOverTimeDRepository
    {
        private readonly ApplicationDbContext _context;

        public HrOverTimeDRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
