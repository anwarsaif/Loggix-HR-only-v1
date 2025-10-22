using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttShiftTimeTableRepository : GenericRepository<HrAttShiftTimeTable>, IHrAttShiftTimeTableRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAttShiftTimeTableRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
