using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrOverTimeMDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrOverTimeMFilterDto? Filter { get; set; }
		public List<HrOverTimeMVw>? Details { get; set; }
	}
}
