using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Logix.Infrastructure.Mapping.HR;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrVisaRepository : GenericRepository<HrVisa>, IHrVisaRepository
    {
        private readonly ApplicationDbContext _context;

        public HrVisaRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
