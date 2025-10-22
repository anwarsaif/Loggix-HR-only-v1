using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPermissionTypeVwRepository : GenericRepository<HrPermissionTypeVw>, IHrPermissionTypeVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrPermissionTypeVwRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
