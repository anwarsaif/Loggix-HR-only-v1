using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDirectJobRepository : GenericRepository<HrDirectJob, HrDirectJobVw>, IHrDirectJobRepository
    {
        private readonly ApplicationDbContext _context;

        public HrDirectJobRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
