using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrRecruitmentCandidateDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrRecruitmentCandidateFilterDto? Filter { get; set; }
		public List<HrRecruitmentCandidateFilterDto>? Details { get; set; }
	}
}
