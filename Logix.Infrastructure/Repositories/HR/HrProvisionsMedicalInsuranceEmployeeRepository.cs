using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
	public partial class HrProvisionsMedicalInsuranceEmployeeRepository : GenericRepository<HrProvisionsMedicalInsuranceEmployee, HrProvisionsMedicalInsuranceEmployeeVw>, IHrProvisionsMedicalInsuranceEmployeeRepository
	{
        private readonly ApplicationDbContext _context;

        public HrProvisionsMedicalInsuranceEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
