using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
	public partial class HrProvisionsMedicalInsuranceRepository : GenericRepository<HrProvisionsMedicalInsurance, HrProvisionsMedicalInsuranceVw>, IHrProvisionsMedicalInsuranceRepository
	{
        private readonly ApplicationDbContext _context;

        public HrProvisionsMedicalInsuranceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
