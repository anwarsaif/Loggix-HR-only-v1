using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrUnpaidEmployeesDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrUnpaidEmployeesFilter? Filter { get; set; }
		public List<HrUnpaidEmployeesVM>? Details { get; set; }
	}
}
