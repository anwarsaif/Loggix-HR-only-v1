using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEmployeeService : IGenericQueryService<HrEmployeeDto, HrEmployeeVw>, IGenericWriteService<HrEmployeeDto, HrEmployeeEditDto>
    {
        Task<IResult<IEnumerable<HrJobProgramVw>>> GetHrJobProgramVw(Expression<Func<HrJobProgramVw, bool>> expression);
        Task<IResult<IEnumerable<HrAttShift>>> GetHrAttShift(Expression<Func<HrAttShift, bool>> expression);

        Task<IResult<IEnumerable<HrAttendanceReportDto>>> GetHrAttendanceReport(string EmpCode = "", long BranchId = 0, long TimeTableId = 0, int StatusId = 0, long Location = 0, long DeptId = 0, int AttendanceType = 0, int SponsorsId = 0);
        Task<IResult<bool>> CHeckEmpInBranch(long? EmpID);
        Task<IResult<List<HrEmployeeVw>>> Search(EmployeeFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrEmployeeBendingVM>>> EmployeeBendingSearch(HrEmployeeBendingFilterVM filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrUnpaidEmployeesVM>>> UnpaidEmployeesSearch(HrUnpaidEmployeesFilter filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrEmployeeVw>>> HRQualificationsSearch(HrQualificationsFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrEmployeeVw>>> SearchEmployeeSub(EmployeeSubFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrEmployeeFileFilterDto>>> SearchEmployeeFile(HrEmployeeFileFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HREmpIDExpireReportFilterDto>>> SearchEmpIDExpireReport(HREmpIDExpireReportFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<RPPassportFilterDto>>> SearchRPPassport(RPPassportFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrRPContractFilterDto>>> SearchRPContract(HrRPContractFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<RPMedicalInsuranceFilterDto>>> SearchRPMedicalInsurance(RPMedicalInsuranceFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<DOAppointmentFilterDto>>> SearchRPDOAppointement(DOAppointmentFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<RPAttendFilterDto>>> SearchRPAttend(RPAttendFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<RPBankFilterDto>>> SearchRPBank(RPBankFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrStaffSalariesAllowancesDeductionsFilterDto>>> SearchHrStaffSalariesAllowancesDeductions(HrStaffSalariesAllowancesDeductionsFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<HrEmployee>> GetEmpByCode(string EmpCode, long facilityId);
        Task<IResult<List<string>>> GetchildDepartment(long DeptId, CancellationToken cancellationToken = default);
    }

}
