using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrPayrollByBranchDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrPayrollFilterDto? Filter { get; set; }
		public List<PayrollAccountingEntryResultDto>? Details { get; set; }
	}
}
