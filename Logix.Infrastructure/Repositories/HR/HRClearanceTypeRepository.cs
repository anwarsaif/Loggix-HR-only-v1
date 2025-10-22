using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrClearanceTypeRepository : GenericRepository<HrClearanceType>, IHrClearanceTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrClearanceTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }   
   


}
