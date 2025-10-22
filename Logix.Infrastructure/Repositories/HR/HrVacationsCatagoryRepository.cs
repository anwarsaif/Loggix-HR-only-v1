using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrVacationsCatagoryRepository : GenericRepository<HrVacationsCatagory>, IHrVacationsCatagoryRepository
    {
        private readonly ApplicationDbContext _context;

        public HrVacationsCatagoryRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }

}
