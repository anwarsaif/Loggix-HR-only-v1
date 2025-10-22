using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrClearanceMonthRepository : GenericRepository<HrClearanceMonth>, IHrClearanceMonthRepository
    {
        private readonly ApplicationDbContext _context;

        public HrClearanceMonthRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
   


}
