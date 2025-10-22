using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrApprovalAbsenceDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HRApprovalAbsencesReportFilterDto? Filter { get; set; }
		public List<HRApprovalAbsencesReportDto>? Details { get; set; }
	}
}
