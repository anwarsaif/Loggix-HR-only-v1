using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{

    public class HrAllowanceDeductionMRepository : GenericRepository<HrAllowanceDeductionM>, IHrAllowanceDeductionMRepository

    {
        private readonly ApplicationDbContext _context;

        public HrAllowanceDeductionMRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
   


}
