using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class HRLoanReportDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public HrLoanFilterDto? Filter { get; set; }
    public List<HrLoanFilterDto>? Details { get; set; }
  }
}
