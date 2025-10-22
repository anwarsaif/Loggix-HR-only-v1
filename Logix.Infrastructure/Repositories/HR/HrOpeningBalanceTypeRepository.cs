using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Infrastructure.DbContexts;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrOpeningBalanceTypeRepository : GenericRepository<HrOpeningBalanceType>, IHrOpeningBalanceTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrOpeningBalanceTypeRepository(ApplicationDbContext context, ICurrentData session) : base(context)
        {
            this._context = context;
        }

    }

}
