using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttTimeTableRepository : GenericRepository<HrAttTimeTable>, IHrAttTimeTableRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAttTimeTableRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
