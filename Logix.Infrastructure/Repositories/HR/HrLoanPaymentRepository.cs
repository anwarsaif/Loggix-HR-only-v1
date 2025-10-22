using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLoanPaymentRepository : GenericRepository<HrLoanPayment>, IHrLoanPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public HrLoanPaymentRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
