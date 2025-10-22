using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDecisionsEmployeeRepository : GenericRepository<HrDecisionsEmployee>, IHrDecisionsEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrDecisionsEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
   


}
