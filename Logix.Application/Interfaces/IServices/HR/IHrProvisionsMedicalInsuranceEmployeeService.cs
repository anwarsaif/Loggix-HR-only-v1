using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
	public interface IHrProvisionsMedicalInsuranceEmployeeService : IGenericQueryService<HrProvisionsMedicalInsuranceEmployeeDto, HrProvisionsMedicalInsuranceEmployeeVw>, IGenericWriteService<HrProvisionsMedicalInsuranceEmployeeDto, HrProvisionsMedicalInsuranceEmployeeEditDto>
    {

    }

}
