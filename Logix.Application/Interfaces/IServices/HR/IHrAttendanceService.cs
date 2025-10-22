using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttendanceService : IGenericQueryService<HrAttendanceDto, HrAttendancesVw>, IGenericWriteService<HrAttendanceDto, HrAttendanceEditDto>
    {
        Task<IResult<IEnumerable<HRAttendanceReportDto>>> getHR_Attendance_Report_SP(HRAttendanceReportFilterDto filter);

        Task<IResult<string>> HR_Attendance_SP_CmdType_1(HrAttendanceDto entity, CancellationToken cancellationToken = default);

        Task<IResult<IEnumerable<HRAttendanceReport5Dto>>> getHR_Attendance_Report5_SP(HRAttendanceReport5FilterDto filter);
        Task<IResult<IEnumerable<HRAttendanceReport4Dto>>> getHR_Attendance_Report4_SP(HRAttendanceReport4FilterDto filter);
        Task<IResult<IEnumerable<HrAttendancesFilterDto>>> AttendanceSearch(HrAttendancesFilterDto filter);
        Task<IResult<IEnumerable<HRAddMultiAttendanceDto>>> AttendanceSearchForMultiAdd(HRAddMultiAttendanceFilterDto filter);
        Task<IResult<AddMultiAttendanceResultDto>> MultiAdd(List<HrMultiAddDto> entity, CancellationToken cancellationToken = default);
        Task<IResult<AddAttendanceFromExcelResultDto>> AddAttendanceFromExcel(IEnumerable<AddAttendanceFromExcelDto> entities, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HRAttendanceTotalReportDto>>> Attendance_TotalReport(HRAttendanceTotalReportFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HRAttendanceTotalReportNewSPDto>>> HR_Attendance_TotalReportNew_SP(HRAttendanceTotalReportFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HRAttendanceReport6SP>>> HR_Attendance_Report6_SP(HRAttendanceReport6FilterSP filter, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HRAttendanceCheckingStaffFilterDto>>> GetEmployeesForUploadAttendances(HRAttendanceCheckingStaffFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<string>> UploadAttendances(HRAttendanceUploadDto obj, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<AttendanceSummaryDto>>> GetAttendanceReportByDate(AttendanceSummaryFilter filter, CancellationToken cancellationToken = default);
        Task<IResult<string>> AddDelayFromReport(List<AddDelayDto> entities, CancellationToken cancellationToken = default);
        Task<IResult<string>> HR_Reaset_Attendance_SP(HrAttendanceResetDto entity, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HRAttendanceTotalReportSPDto>>> HR_Attendance_TotalReport_SP(HRAttendanceTotalReportSPFilterDto filter);
		Task<IResult<List<HRAttendanceReport5Dto>>> Search(HRAttendanceReport5FilterDto filter, CancellationToken cancellationToken = default);

	}


}
