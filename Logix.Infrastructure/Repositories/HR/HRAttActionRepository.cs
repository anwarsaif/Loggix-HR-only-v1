using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttActionRepository : GenericRepository<HrAttAction>, IHrAttActionRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAttActionRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 



}
