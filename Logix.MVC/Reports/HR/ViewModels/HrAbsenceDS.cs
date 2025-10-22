using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrAbsenceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrAbsenceFilterDto? Filter { get; set; }
		public List<HrAbsenceFilterDto>? Details { get; set; }
	}
}
