using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrVacationBalanceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrVacationBalanceFilterDto? Filter { get; set; }
		public List<HrVacationBalanceFilterDto>? Details { get; set; }
	}
}
