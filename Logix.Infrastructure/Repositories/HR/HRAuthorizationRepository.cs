using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAuthorizationRepository : GenericRepository<HrAuthorization>, IHrAuthorizationRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAuthorizationRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }  
   


}
