using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrEmpWorkTimeRepository : GenericRepository<HrEmpWorkTime>, IHrEmpWorkTimeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrEmpWorkTimeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
