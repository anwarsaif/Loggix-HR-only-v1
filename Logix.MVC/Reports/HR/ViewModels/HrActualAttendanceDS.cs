using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrActualAttendanceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrCheckInOutFilterDto? Filter { get; set; }
		public List<HrActualAttendanceReportDto>? Details { get; set; }
	}
}
