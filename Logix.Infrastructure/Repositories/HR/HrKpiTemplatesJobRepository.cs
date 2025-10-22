using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrKpiTemplatesJobRepository : GenericRepository<HrKpiTemplatesJob>, IHrKpiTemplatesJobRepository
    {
        private readonly ApplicationDbContext context;

        public HrKpiTemplatesJobRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
