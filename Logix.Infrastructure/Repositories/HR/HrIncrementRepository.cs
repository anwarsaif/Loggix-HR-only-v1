using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrIncrementRepository : GenericRepository<HrIncrement, HrIncrementsVw>, IHrIncrementRepository
    {
        private readonly ApplicationDbContext _context;

        public HrIncrementRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
