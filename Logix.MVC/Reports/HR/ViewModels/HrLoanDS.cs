using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrLoanDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrLoanFilterDto? Filter { get; set; }
		public List<HrLoanFilterDto>? Details { get; set; }
	}
}
