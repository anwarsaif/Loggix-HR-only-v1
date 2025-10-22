using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttLocationRepository : GenericRepository<HrAttLocation>, IHrAttLocationRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAttLocationRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
