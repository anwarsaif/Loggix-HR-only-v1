using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrEmployeeBendingDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrEmployeeBendingFilterVM? Filter { get; set; }
		public List<HrEmployeeBendingVM>? Details { get; set; }
	}
}
