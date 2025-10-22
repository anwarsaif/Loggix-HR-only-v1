using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrExpenseRepository : GenericRepository<HrExpense>, IHrExpenseRepository
    {
        private readonly ApplicationDbContext context;

        public HrExpenseRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
