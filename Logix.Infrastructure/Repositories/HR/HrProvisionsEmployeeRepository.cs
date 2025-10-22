using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{

    public class HrProvisionsEmployeeRepository : GenericRepository<HrProvisionsEmployee, HrProvisionsEmployeeVw>, IHrProvisionsEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public HrProvisionsEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}

