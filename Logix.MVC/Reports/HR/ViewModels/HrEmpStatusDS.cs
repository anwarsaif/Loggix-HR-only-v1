using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrEmpStatusDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRRPEmpStatusHistoryFilterDto? Filter { get; set; }
		public List<HRRPEmpStatusHistoryFilterDto>? Details { get; set; }
	}
}
