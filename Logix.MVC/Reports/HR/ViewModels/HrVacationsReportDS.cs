using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrVacationsReportDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrVacationsFilterDto? Filter { get; set; }
		public List<HrVacationsFilterDto>? Details { get; set; }
	}
}
