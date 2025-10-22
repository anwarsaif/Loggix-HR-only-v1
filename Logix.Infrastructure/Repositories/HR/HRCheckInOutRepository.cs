using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCheckInOutRepository : GenericRepository<HrCheckInOut, HrCheckInOutVw>, IHrCheckInOutRepository
    {
        private readonly ApplicationDbContext _context;

        public HrCheckInOutRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }  
   


}
