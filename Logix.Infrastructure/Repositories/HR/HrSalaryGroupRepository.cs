using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrSalaryGroupRepository : GenericRepository<HrSalaryGroup>, IHrSalaryGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public HrSalaryGroupRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
}
