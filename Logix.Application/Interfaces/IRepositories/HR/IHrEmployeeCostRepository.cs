using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrEmployeeCostRepository : IGenericRepository<HrEmployeeCost, HrEmployeeCostVw>
    {
        Task<IEnumerable<dynamic>> GetRPEmployeeContract(string EmpCode, string EmpName, string nationalityId, string DeptId, string Location, int LanguageType, string BranchID, string ?BranchsID, string StatusID);
        Task<IEnumerable<dynamic>> GetRPEmployeeCost(string EmpCode, string EmpName, string nationalityId, string StartDate, string EndDate, string DeptId, string Location, int LanguageType);

    }

}
