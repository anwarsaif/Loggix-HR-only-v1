using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrEmpGoalIndicatorRepository : GenericRepository<HrEmpGoalIndicator>, IHrEmpGoalIndicatorRepository
    {
        private readonly ApplicationDbContext _context;

        public HrEmpGoalIndicatorRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
