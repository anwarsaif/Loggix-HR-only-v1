using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrCurrBalanceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public CurrentBalanceFilterDto? Filter { get; set; }
		public List<OtherBalanceDto>? Details { get; set; }
	}
}
