using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
	public interface IHrJobAllowanceDeductionService : IGenericQueryService<HrJobAllowanceDeductionDto, HrJobAllowanceDeductionVw>, IGenericWriteService<HrJobAllowanceDeductionDto, HrJobAllowanceDeductionDto>
    {
		Task<IResult<List<HrJobAllowanceDeductionDto>>> AddRange(List<HrJobAllowanceDeductionDto> entities, CancellationToken cancellationToken = default);


	}
}
