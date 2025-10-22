using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRequestRepository : GenericRepository<HrRequest>, IHrRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRequestRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
