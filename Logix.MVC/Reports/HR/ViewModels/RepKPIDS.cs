using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class RepKPIDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRRepKPIFilterDto? Filter { get; set; }
		public List<HRRepKPIFilterDto>? Details { get; set; }
	}
}
