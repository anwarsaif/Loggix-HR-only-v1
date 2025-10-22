using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrBalanceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrOpeningBalanceFilterDto? Filter { get; set; }
		public List<HrOpeningBalanceFilterDto>? Details { get; set; }
	}
}
