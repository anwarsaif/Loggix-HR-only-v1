using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrExpensesTypeRepository : GenericRepository<HrExpensesType>, IHrExpensesTypeRepository
    {
        private readonly ApplicationDbContext context;

        public HrExpensesTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
