using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDefinitionSalaryEmpRepository : GenericRepository<HrDefinitionSalaryEmp>, IHrDefinitionSalaryEmpRepository
    {
        private readonly ApplicationDbContext _context;

        public HrDefinitionSalaryEmpRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
