using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public partial class HrProvisionRepository : GenericRepository<HrProvision>, IHrProvisionRepository
    {
        private readonly ApplicationDbContext _context;

        public HrProvisionRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
