using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrContracteRepository : GenericRepository<HrContracte>, IHrContracteRepository
    {
        private readonly ApplicationDbContext _context;

        public HrContracteRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
   


}
