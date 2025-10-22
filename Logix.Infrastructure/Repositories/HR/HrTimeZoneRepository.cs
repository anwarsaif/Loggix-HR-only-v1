using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrTimeZoneRepository : GenericRepository<HrTimeZone>, IHrTimeZoneRepository
    {
        private readonly ApplicationDbContext _context;

        public HrTimeZoneRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
