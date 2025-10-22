using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrIncrementDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrIncrementFilterDto? Filter { get; set; }
		public List<HrIncrementsVw>? Details { get; set; }
	}
}
