using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLoanInstallmentPaymentRepository : GenericRepository<HrLoanInstallmentPayment>, IHrLoanInstallmentPaymentRepository
    {
        private readonly ApplicationDbContext context;

        public HrLoanInstallmentPaymentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
