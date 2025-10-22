using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{

	public class HrAllowanceDeductionTempOrFixRepository : GenericRepository<HrAllowanceDeductionTempOrFix>, IHrAllowanceDeductionTempOrFixRepository

    {
        private readonly ApplicationDbContext _context;

        public HrAllowanceDeductionTempOrFixRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }  


}
