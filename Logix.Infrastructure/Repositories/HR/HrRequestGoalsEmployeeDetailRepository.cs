using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRequestGoalsEmployeeDetailRepository : GenericRepository<HrRequestGoalsEmployeeDetail>, IHrRequestGoalsEmployeeDetailRepository
    {
        public HrRequestGoalsEmployeeDetailRepository(ApplicationDbContext context) : base(context)
        {

        }

    }



}
