using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrAttendancesFilterDto? Filter { get; set; }
		public List<HrAttendancesFilterDto>? Details { get; set; }
	}
}
