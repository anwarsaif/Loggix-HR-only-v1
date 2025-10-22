using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceUnknownDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrAttendanceUnknownFilterDto? Filter { get; set; }
		public List<HrAttendanceUnknownFilterDto>? Details { get; set; }
	}
}
