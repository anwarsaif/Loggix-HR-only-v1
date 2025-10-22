using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using Logix.MVC.LogixAPIs.HR.ViewModel;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrDisciplinaryCaseActionEditDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		//public HrDisciplinaryCaseActionFilterVM? Filter { get; set; }
		public List<HrDisciplinaryCaseActionVw>? Details { get; set; }
	}
}
