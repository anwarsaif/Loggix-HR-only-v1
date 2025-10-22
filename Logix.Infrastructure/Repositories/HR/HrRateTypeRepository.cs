using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRateTypeRepository : GenericRepository<HrRateType>, IHrRateTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRateTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }

}
