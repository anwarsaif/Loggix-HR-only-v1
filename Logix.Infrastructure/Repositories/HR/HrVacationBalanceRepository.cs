using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrVacationBalanceRepository : GenericRepository<HrVacationBalance, HrVacationBalanceVw>, IHrVacationBalanceRepository
    {
        private readonly ApplicationDbContext _context;

        public HrVacationBalanceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
