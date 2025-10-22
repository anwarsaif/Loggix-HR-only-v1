using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceTotalFromToDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRAttendanceTotalReportFilterDto? Filter { get; set; }
		public List<HRAttendanceTotalReportNewSPDto>? Details { get; set; }
	}
}
