using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrMandateLocationMasterRepository : GenericRepository<HrMandateLocationMaster>, IHrMandateLocationMasterRepository
    {
        private readonly ApplicationDbContext _context;

        public HrMandateLocationMasterRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
