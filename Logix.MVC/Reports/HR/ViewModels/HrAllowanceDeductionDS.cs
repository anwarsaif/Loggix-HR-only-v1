using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAllowanceDeductionDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrAllowanceDeductionOtherFilterDto? Filter { get; set; }
		public List<HrAllowanceDeductionVw>? Details { get; set; }
	}
}
