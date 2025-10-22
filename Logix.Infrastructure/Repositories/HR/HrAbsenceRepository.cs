using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAbsenceRepository : GenericRepository<HrAbsence, HrAbsenceVw>, IHrAbsenceRepository
    {
        private readonly ApplicationDbContext _context;

        public HrAbsenceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
