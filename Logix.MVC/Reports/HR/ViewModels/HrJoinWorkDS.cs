using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrJoinWorkDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrDirectJobFilterDto? Filter { get; set; }
		public List<HrDirectJobVw>? Details { get; set; }
	}
}
