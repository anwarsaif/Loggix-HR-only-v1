using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrInsuranceEmpRepository : GenericRepository<HrInsuranceEmp, HrInsuranceEmpVw>, IHrInsuranceEmpRepository
    {
        private readonly ApplicationDbContext _context;

        public HrInsuranceEmpRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    } 
 


}
