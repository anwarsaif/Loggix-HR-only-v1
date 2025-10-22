using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPolicyRepository : GenericRepository<HrPolicy>, IHrPolicyRepository
    {
        private readonly ApplicationDbContext _context;

        public HrPolicyRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }



}
