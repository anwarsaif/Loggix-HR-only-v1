using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrInsurancePolicyRepository : GenericRepository<HrInsurancePolicy>, IHrInsurancePolicyRepository
    {
        private readonly ApplicationDbContext _context;

        public HrInsurancePolicyRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
 


}
