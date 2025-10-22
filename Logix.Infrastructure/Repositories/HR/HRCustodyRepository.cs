using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCustodyRepository : GenericRepository<HrCustody>, IHrCustodyRepository
    {
        private readonly ApplicationDbContext _context;

        public HrCustodyRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
   


}
