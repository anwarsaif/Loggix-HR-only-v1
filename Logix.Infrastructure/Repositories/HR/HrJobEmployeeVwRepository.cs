using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrJobEmployeeVwRepository : GenericRepository<HrJobEmployeeVw>, IHrJobEmployeeVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrJobEmployeeVwRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
