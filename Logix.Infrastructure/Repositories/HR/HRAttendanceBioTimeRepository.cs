using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttendanceBioTimeRepository : GenericRepository<HrAttendanceBioTime>, IHrAttendanceBioTimeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAttendanceBioTimeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }  
   


}
