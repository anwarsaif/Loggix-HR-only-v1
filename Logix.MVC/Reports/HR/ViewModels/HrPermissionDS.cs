using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrPermissionDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrPermissionFilterDto? Filter { get; set; }
		public List<HrPermissionFilterDto>? Details { get; set; }
	}
}
