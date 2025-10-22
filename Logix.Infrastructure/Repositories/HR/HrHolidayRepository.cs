using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrHolidayRepository : GenericRepository<HrHoliday>, IHrHolidayRepository
    {
        private readonly ApplicationDbContext _context;

        public HrHolidayRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
