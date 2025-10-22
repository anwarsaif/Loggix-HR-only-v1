using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrVacationsDayTypeRepository : GenericRepository<HrVacationsDayType>, IHrVacationsDayTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrVacationsDayTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
