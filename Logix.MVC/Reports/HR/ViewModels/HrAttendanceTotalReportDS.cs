using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceTotalReportDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRAttendanceTotalReportSPFilterDto? Filter { get; set; }
		public List<HRAttendanceTotalReportSPDto>? Details { get; set; }
	}
}
