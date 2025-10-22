using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttLocationEmployeeRepository : GenericRepository<HrAttLocationEmployee>, IHrAttLocationEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAttLocationEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
 


}
