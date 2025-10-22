using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
	public interface IHrProvisionsMedicalInsuranceEmployeeAccVwRepository : IGenericRepository<HrProvisionsMedicalInsuranceEmployeeAccVw>
    {
        Task<List<ProvisionsMedicalInsuranceEmployeeAccVwDto>> GetAccDetailsAsync(long id);

    }

}
