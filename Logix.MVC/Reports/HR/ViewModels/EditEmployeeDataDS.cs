using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;
using Logix.Domain.Main;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class EditEmployeeDataDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public List<HrEmployeeVw>? Details { get; set; }
	}
}
