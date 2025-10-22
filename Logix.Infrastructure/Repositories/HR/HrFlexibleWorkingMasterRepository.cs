using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrFlexibleWorkingMasterRepository : GenericRepository<HrFlexibleWorkingMaster>, IHrFlexibleWorkingMasterRepository
    {
        private readonly ApplicationDbContext _context;

        public HrFlexibleWorkingMasterRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
