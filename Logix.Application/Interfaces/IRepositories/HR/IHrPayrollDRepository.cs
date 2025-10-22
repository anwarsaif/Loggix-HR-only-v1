using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrPayrollDRepository : IGenericRepository<HrPayrollD, HrPayrollDVw>
    {
        Task<List<PayrollAccountingEntryDto>> GetHrPayrollDTrans(long msId, long FacilityId);
        Task<List<PayrollAccountingEntryResultDto>> GetPayrollReports(HrPayrollFilterDto filter, int type);
        Task<List<HrPayrollCompareResult>> PayrollCompare(HrPayrollCompareFilterDto filter, int CmdType);
        Task<int> Check_Emp_Exists_In_Payroll(string msDate, long payrollTypeId, long empId);

    }

}
