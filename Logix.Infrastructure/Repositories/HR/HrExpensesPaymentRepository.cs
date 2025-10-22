using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrExpensesPaymentRepository : GenericRepository<HrExpensesPayment>, IHrExpensesPaymentRepository
    {
        private readonly ApplicationDbContext context;

        public HrExpensesPaymentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
       
    }
}
