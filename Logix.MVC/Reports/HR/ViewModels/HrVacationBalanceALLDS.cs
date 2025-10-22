using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrVacationBalanceALLDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrVacationBalanceALLSendFilterDto? Filter { get; set; }
		public List<HrVacationBalanceALLFilterDto>? Details { get; set; }
	}
}
