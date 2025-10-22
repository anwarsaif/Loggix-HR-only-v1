using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HRRPVacationEmployeeDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrRPVacationEmployeeFilterDto? Filter { get; set; }
		public List<HrRPVacationEmployeeFilterDto>? Details { get; set; }
	}
}
