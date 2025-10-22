using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
	public interface IHrAllReportsService
	{

		//      Task<IResult> NewRemove(long AbsenceId, CancellationToken cancellationToken = default);
		//      Task<IResult> Remove(List<long> AbsenceIds, CancellationToken cancellationToken = default);
		//      Task<IResult> DisciplinaryCaseIDChanged(string EmpCode,string AbsenceDate,int DisciplinaryCaseID, CancellationToken cancellationToken = default);
		//      Task<IResult<HrAbsenceDto>> AddSingleAbsence(HrAbsenceAddDto entity, CancellationToken cancellationToken = default);
		//      Task<IResult<AddAbsenceFromExcelResultDto>> AddAbsenceFromExcel(List<HrAbsenceAddDto> entity, CancellationToken cancellationToken = default);
		//      Task<IResult<string>> AbsenceNotAttendance(AbsenceNotAttendanceDto entity, CancellationToken cancellationToken = default);
		//      Task<IResult<string>> AbsenceForNewInterval(HrAbsenceAddDto entity, CancellationToken cancellationToken = default);
		//      Task<IResult<AddMultiAbsenceAddResultDto>> MultiAbsenceAdd(HrMultiAbsenceAddDto entity, CancellationToken cancellationToken = default);
		//      Task<IResult<IEnumerable<HRApprovalAbsencesReportDto>>> HRApprovalAbsencesReport(HRApprovalAbsencesReportFilterDto filter);
		//Task<IResult<List<HrAbsenceFilterDto>>> Search(HrAbsenceFilterDto filter, CancellationToken cancellationToken = default);
		Task<HrPayrollCompareFilterDto> GetHrCompareByBranch(Dictionary<string, string> filtersDictionary);
	}

}
