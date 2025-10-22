using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAttendanceDataDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRAttendanceReport4FilterDto? Filter { get; set; }
		public List<HRAttendanceReport4Dto>? Details { get; set; }
	}
}
