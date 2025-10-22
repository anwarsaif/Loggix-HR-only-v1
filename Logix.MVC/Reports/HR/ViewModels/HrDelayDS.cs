using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrDelayDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrDelayFilterDto? Filter { get; set; }
		public List<HrDelayFilterDto>? Details { get; set; }
	}
}
