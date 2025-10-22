using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCustodyItemRepository : GenericRepository<HrCustodyItem>, IHrCustodyItemRepository
    {
        private readonly ApplicationDbContext _context;

        public HrCustodyItemRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
 


}
