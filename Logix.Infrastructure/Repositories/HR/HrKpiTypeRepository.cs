using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrKpiTypeRepository : GenericRepository<HrKpiType>, IHrKpiTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrKpiTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
