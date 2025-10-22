using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceReportDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRAttendanceReportFilterDto? Filter { get; set; }
		public List<HRAttendanceReportDto>? Details { get; set; }
	}
}
