using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.TS;
using Logix.Application.Services.TS;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPayrollDService : IGenericQueryService<HrPayrollDDto, HrPayrollDVw>, IGenericWriteService<HrPayrollDDto, HrPayrollDEditDto>
    {
        Task<IResult<List<PayrollAccountingEntryDto>>> GetHrPayrollDTrans(long msId, long FacilityId);
        //Task<IResult<List<PayrollAccountingEntryResultDto>>> GetPayrollReports(HrPayrollFilterDto filter, int type);
        Task<IResult<List<HrPayrollCompareResult?>>> PayrollCompare(HrPayrollCompareFilterDto filter, int CmdType);

        Task<IResult<HrDashboard2ResultDto>> GetPayrollReportsForHrDashboard2(HrDashboardDto filter);
		Task<IResult<List<PayrollAccountingEntryResultDto>>> Search(HrPayrollFilterDto filter, CancellationToken cancellationToken = default);
		Task<IResult<List<PayrollAccountingEntryResultDto>>> PayrollByLocationSearch(HrPayrollFilterDto filter, CancellationToken cancellationToken = default);
		Task<IResult<List<PayrollAccountingEntryResultDto>>> PayrollByDeptSearch(HrPayrollFilterDto filter, CancellationToken cancellationToken = default);
		Task<IResult<List<HrPayrollDVw>>> SearchPayrollQuery(HrPayrollQueryFilterDto filter, CancellationToken cancellationToken = default);
		Task<IResult<List<HrPayrollCompareResult>>> SearchComperByBranch(HrPayrollCompareFilterDto filter, CancellationToken cancellationToken = default);


	}


}
