using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrCompareByBranchDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrPayrollCompareFilterDto? Filter { get; set; }
		public List<HrPayrollCompareResult>? Details { get; set; }
	}
}
