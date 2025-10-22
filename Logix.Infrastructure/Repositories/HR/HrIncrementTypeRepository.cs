using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrIncrementTypeRepository : GenericRepository<HrIncrementType>, IHrIncrementTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrIncrementTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
