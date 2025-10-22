using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrEmployeeRepository : IGenericRepository<HrEmployee, HrEmployeeVw>
    {
        Task<IEnumerable<HrJobProgramVw>> GetHrJobProgramVw(Expression<Func<HrJobProgramVw, bool>> expression);
        Task<IEnumerable<HrJobVw>> GetHrJobVw(Expression<Func<HrJobVw, bool>> expression);
        Task<IEnumerable<HrAttShift>> GetHrAttShift(Expression<Func<HrAttShift, bool>> expression);

        Task<IEnumerable<HrAttendanceReportDto>> GetHrAttendanceReport(string EmpCode = "", long BranchId = 0, long TimeTableId = 0, int StatusId = 0, long Location = 0, long DeptId = 0, int AttendanceType = 0, int SponsorsId = 0);
        Task<long> GetEmpId(long facilityId, string EmpCode);
        Task<int> chkEmpid(string EmpCode);
        Task<int> CkeckEmpStatus(string EmpCode, long facilityId);
        Task<HrEmployee> GetEmpByCode(string EmpCode, long facilityId);

    }
}
