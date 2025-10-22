using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrVisitStepRepository : GenericRepository<HrVisitStep>, IHrVisitStepRepository
    {
        public HrVisitStepRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
