using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrEndOfServiceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrLeaveFilterDto? Filter { get; set; }
		public List<HrLeaveFilterDto>? Details { get; set; }
	}
}
