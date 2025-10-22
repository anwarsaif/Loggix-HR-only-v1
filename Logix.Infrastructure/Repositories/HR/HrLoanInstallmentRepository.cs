using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLoanInstallmentRepository : GenericRepository<HrLoanInstallment, HrLoanInstallmentVw>, IHrLoanInstallmentRepository
    {
        private readonly ApplicationDbContext context;

        public HrLoanInstallmentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
