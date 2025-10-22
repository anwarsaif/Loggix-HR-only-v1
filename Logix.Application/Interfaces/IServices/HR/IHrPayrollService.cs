using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPayrollService : IGenericQueryService<HrPayrollDto, HrPayrollVw>, IGenericWriteService<HrPayrollDto, HrPayrollEditDto>
    {
        Task<IResult<IEnumerable<HRPayrollManuallCreateSpDto>>> getHR_Payroll_Create2_Sp(HRPayrollCreate2SpFilterDto filter);
        Task<IResult<string>> AddNewPayroll(HrPayrollAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> AddNewAutomaticPayroll1(HrPayrollAddDto entity, CancellationToken cancellationToken = default);

        Task<IResult<IEnumerable<HRPayrollCreate2SpDto>>> getHR_Payroll_Create_Sp(HRPayrollCreateSpFilterDto filter);
        Task<IResult<string>> CheckJoinWorkForPayroll(HRPayrollCreateSpFilterDto entity, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HrPreparationSalariesVw>>> CommissionPayrollSearch(HRPayrollCreateSpFilterDto filter);
        Task<IResult<string>> AddNewCommissionPayroll(HrCommissionPayrollAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> AddNewPaymentDues(HrPaymentDueAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HRPreparationSalariesLoanDto>>> getHR_Preparation_Salaries_Loan_SP(HRPreparationSalariesLoanFilterDto filter);
        Task<IResult<HrPayrollApprovedDto>> BindApprovedPayroll(long msId);
        Task<IResult<string>> AddNewApprovedPayroll1(HrPayrollApprovedFilterAndAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> ChangeStatusPayroll(ApproveRejectPayrollDto entity, int state, CancellationToken cancellationToken = default);
        Task<IResult<string>> ChangeStatusPayroll(string msIds, int state, CancellationToken cancellationToken = default);


        Task<IResult<IEnumerable<HRPayrollAdvancedResultDto>>> HR_Payroll_Create_Advanced_Sp(HRPayrollCreateSpFilterDto filter);

        Task<IResult<string>> AddNewAdvancedPayroll(HrPayrollAdvancedAddDto entity, CancellationToken cancellationToken = default);

        // مسير خارج دوام
        Task<IResult<string>> PayrollOverTimeAdd(PayrollOverTimeAddDto entity, CancellationToken cancellationToken = default);

        // مسير بدل سكن
        Task<IResult<string>> PayrollHousingAllowanceAdd(PayrollHousingAllowancesAddDto entity, CancellationToken cancellationToken = default);
        //  مسير انتداب
        Task<IResult<string>> PayrollMandateAdd(PayrollMandateAddDto entity, CancellationToken cancellationToken = default);


        // مسير تذاكر مستحقة
        Task<IResult<string>> PayrollTicketAllowanceAdd(PayrollTicketAllowancesAddDto entity, CancellationToken cancellationToken = default);


        //  مسير خارج دوام يدوي 
        Task<IResult<string>> PayrollOverTime2Add(PayrollOverTime2AddDto entity, CancellationToken cancellationToken = default);

        //  مسير دوام مرن
        Task<IResult<string>> PayrollFlexibleWorkingAdd(PayrollFlexibleWorkingAddDto entity, CancellationToken cancellationToken = default);

    }


}
