using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceReportDaysDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRAttendanceReport6FilterSP? Filter { get; set; }
		public List<HRAttendanceReport6SP>? Details { get; set; }
	}
}
