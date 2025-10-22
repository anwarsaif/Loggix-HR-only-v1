using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrGosiEmployeeAccVwRepository : GenericRepository<HrGosiEmployeeAccVw>, IHrGosiEmployeeAccVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrGosiEmployeeAccVwRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
