using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrMandateRequestsMasterRepository : GenericRepository<HrMandateRequestsMaster>, IHrMandateRequestsMasterRepository
    {
        private readonly ApplicationDbContext context;

        public HrMandateRequestsMasterRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
