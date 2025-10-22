using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrCheckingStaffDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRAttendanceCheckingStaffFilterDto? Filter { get; set; }
		public List<HRAttendanceCheckingStaffFilterDto>? Details { get; set; }
	}
}
