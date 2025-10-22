using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPayrollCostcenterRepository : GenericRepository<HrPayrollCostcenter>, IHrPayrollCostcenterRepository
    {
        private readonly ApplicationDbContext context;

        public HrPayrollCostcenterRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }



}
