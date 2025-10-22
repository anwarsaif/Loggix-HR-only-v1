using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrEmployeeDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public EmployeeFilterDto? Filter { get; set; }
		public List<HrEmployeeVw>? Details { get; set; }
	}
}
