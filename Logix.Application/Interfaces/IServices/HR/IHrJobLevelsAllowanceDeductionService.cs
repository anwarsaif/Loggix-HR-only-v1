using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
	public interface IHrJobLevelsAllowanceDeductionService : IGenericQueryService<HrJobLevelsAllowanceDeductionDto, HrJobLevelsAllowanceDeductionVw>, IGenericWriteService<HrJobLevelsAllowanceDeductionDto, HrJobLevelsAllowanceDeductionEditDto>
    {

	}
}
