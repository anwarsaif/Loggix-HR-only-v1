using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrClearanceRepository : GenericRepository<HrClearance, HrClearanceVw>, IHrClearanceRepository
    {
        private readonly ApplicationDbContext _context;

        public HrClearanceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
