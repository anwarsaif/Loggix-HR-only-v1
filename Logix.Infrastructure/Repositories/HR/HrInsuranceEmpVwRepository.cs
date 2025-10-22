using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
	public class HrInsuranceEmpVwRepository : GenericRepository<HrInsuranceEmpVw>, IHrInsuranceEmpVwRepository
	{
        private readonly ApplicationDbContext _context;

        public HrInsuranceEmpVwRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
 


}
