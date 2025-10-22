using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAssignmenRepository : GenericRepository<HrAssignman>, IHrAssignmenRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAssignmenRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
