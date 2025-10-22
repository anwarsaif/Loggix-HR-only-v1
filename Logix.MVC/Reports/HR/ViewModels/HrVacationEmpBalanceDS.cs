using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrVacationEmpBalanceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrVacationEmpBalanceDto? Filter { get; set; }
		public List<HrVacationEmpBalanceDto>? Details { get; set; }
	}
}
