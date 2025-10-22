using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrKpiDetaileRepository : GenericRepository<HrKpiDetaile, HrKpiDetailesVw>, IHrKpiDetaileRepository
    {
        private readonly ApplicationDbContext _context;

        public HrKpiDetaileRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
