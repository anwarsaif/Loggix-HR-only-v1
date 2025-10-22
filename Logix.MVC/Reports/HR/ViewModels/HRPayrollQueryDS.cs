using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HRPayrollQueryDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrPayrollQueryFilterDto? Filter { get; set; }
		public List<HrPayrollDVw>? Details { get; set; }
	}
}
