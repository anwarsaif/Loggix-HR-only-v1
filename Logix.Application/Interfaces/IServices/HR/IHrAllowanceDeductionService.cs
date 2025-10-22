using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAllowanceDeductionService : IGenericQueryService<HrAllowanceDeductionDto, HrAllowanceDeductionVw>, IGenericWriteService<HrAllowanceDeductionDto, HrAllowanceDeductionEditDto>
    {

        Task<IResult<HrAllowanceDeductionDto>> AddOneEdit(HrAllowanceDeductionExtraVM entity, CancellationToken cancellationToken = default);
        Task<IResult<HrAllowanceDeductionDto>> AddYearlyAllowanceDeduction(HrAllowanceDeductionDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrAllowanceDeductionDto>> RemoveOtherDeductionAllowance(long Id, CancellationToken cancellationToken = default);
        Task<IResult<string>> RemoveOtherDeductionAllowance(List<long> Ids, CancellationToken cancellationToken = default);

        // حسميات أو بدلات أخرى
        Task<IResult<HrAllowanceDeductionDto>> AddOtherAllowanceDeduction(HrOtherAllowanceDeductionAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrAllowanceDeductionEditDto>> EditOtherAllowanceDeduction(HrOtherAllowanceDeductionEditDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> MultiAddOtherAllowanceDeduction(HrOtherAllowanceDeductionMultiAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> IntervalAddOtherAllowanceDeduction(HrOtherAllowanceDeductionIntervalAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<AddFromExcelResultDto>> AddOtherAllowanceDeductionFromExcel(List<HrOtherAllowanceDeductionAddFromExcelDto> entities, CancellationToken cancellationToken = default);
        Task<IResult<decimal>> GetTotalAllowances(long EmpId, CancellationToken cancellationToken = default);
        Task<IResult<decimal>> GetTotalDeduction(long EmpId, CancellationToken cancellationToken = default);
		Task<IResult<List<HrAllowanceDeductionVw>>> Search(HrAllowanceDeductionOtherFilterDto filter, CancellationToken cancellationToken = default);

	}
}
