using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrLoanService : IGenericQueryService<HrLoanDto, HrLoanVw>, IGenericWriteService<HrLoanDto, HrLoanEditDto>
    {
        Task<IResult<string>> DeleteHrLoan(long Id, CancellationToken cancellationToken = default);
        Task<Result<HrLoanFilterDto>> GetLoanWithRemainingAmount(long loanId);
        Task<IResult<HrLoanEditDto>> Update(HrEditLoanDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> ReScheduleLoan(InstallmentScheduleDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrLoan4Dto>> Add4(HrLoan4Dto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrLoanDto>> Add2(HrLoanDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrLoanDto>> Add3(HrLoanDto entity, CancellationToken cancellationToken = default);
		    Task<IResult<List<HrLoanFilterDto>>> Search(HrLoanFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<object>> GetSumInstallmentLoanLoanId(HrLoanInstallmentInputDto obj, CancellationToken cancellationToken = default);
        Task<decimal> GetSumInstallmentLoanNotLoanId(HrLoanInstallmentNotLoanIdDto obj, CancellationToken cancellationToken = default);
    }


}
