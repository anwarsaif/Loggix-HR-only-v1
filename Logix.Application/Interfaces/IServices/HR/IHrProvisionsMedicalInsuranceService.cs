using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
	public interface IHrProvisionsMedicalInsuranceService : IGenericQueryService<HrProvisionsMedicalInsuranceDto, HrProvisionsMedicalInsuranceVw>, IGenericWriteService<HrProvisionsMedicalInsuranceDto, HrProvisionsMedicalInsuranceEditDto>
    {
		Task<IResult<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>> GetEmployeeProvisionMedicalInsuranceData(HrProvisionsMedicalInsuranceSearchOnAddFilter filter);
		//Task<IResult<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>> GetEmployeeProvisionEndOfServiceData(HrProvisionsMedicalInsuranceSearchOnAddFilter filter);
		//Task<IResult<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>> GetEmployeeProvisionTicketData(HrProvisionsMedicalInsuranceSearchOnAddFilter filter);
		Task<IResult<string>> CreateProvisionEntry(HrProvisionsMedicalInsuranceEntryAddDto entity, CancellationToken cancellationToken = default);
		Task<IResult<HrProvisionsMedicalInsuranceDto>> AddMedicalInsuranceProvision(HrProvisionsMedicalInsuranceDto entity, CancellationToken cancellationToken = default);
		//Task<IResult<HrProvisionDto>> AddVacationProvision(HrProvisionDto entity, CancellationToken cancellationToken = default);
		//Task<IResult<HrProvisionDto>> AddTicketProvision(HrProvisionDto entity, CancellationToken cancellationToken = default);

	}

}
