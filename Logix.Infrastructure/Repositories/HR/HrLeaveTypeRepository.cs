using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLeaveTypeRepository : GenericRepository<HrLeaveType>, IHrLeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrLeaveTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
