using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEmployeeCostService : IGenericQueryService<HrEmployeeCostDto, HrEmployeeCostVw>, IGenericWriteService<HrEmployeeCostDto, HrEmployeeCostEditDto>
    {
        Task<IResult<string>> Add(HrEmployeeCostDataDto entity, CancellationToken cancellationToken = default);
        Task<IResult<List<HrEmployeeCostFilterDto>>> Search(HrEmployeeCostFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<HrEmployeeCostDataDto>> GetEmpDataByEmpId(string EmpCode, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<dynamic>>> GetRPEmployeeContract(string EmpCode, string EmpName, string nationalityId,  string DeptId, string Location, int LanguageType,string BranchID,string? BranchsID,string StatusID);
        Task<IResult<HrEmployeeCostDataDto>> GetDataById(long Id, CancellationToken cancellationToken = default);
        Task<IResult<string>> Update(HrEmployeeCostDataDto entity, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<dynamic>>> GetRPEmployeeCost(string EmpCode, string EmpName, string nationalityId, string StartDate, string EndDate, string DeptId, string Location, int LanguageType);

    }


}
