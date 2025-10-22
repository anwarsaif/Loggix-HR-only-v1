using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCustodyTypeRepository : GenericRepository<HrCustodyType>, IHrCustodyTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrCustodyTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
   


}
