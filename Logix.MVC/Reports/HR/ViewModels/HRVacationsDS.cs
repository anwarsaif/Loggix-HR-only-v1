using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HRVacationsDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrVacationsFilterDto? Filter { get; set; }
		public List<HrVacationsFilterDto>? Details { get; set; }
	}
}
