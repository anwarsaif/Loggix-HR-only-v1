using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrGosiTypeVwRepository : GenericRepository<HrGosiTypeVw>, IHrGosiTypeVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrGosiTypeVwRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
