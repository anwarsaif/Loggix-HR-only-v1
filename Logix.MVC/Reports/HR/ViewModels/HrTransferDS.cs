using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class HrTransferDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public HrTransferFilterDto? Filter { get; set; }
		public List<HrTransfersVw>? Details { get; set; }
	}
}
