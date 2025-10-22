using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceReportForEmpDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRAttendanceReport5FilterDto? Filter { get; set; }
		public List<HRAttendanceReport5Dto>? Details { get; set; }
	}
}
