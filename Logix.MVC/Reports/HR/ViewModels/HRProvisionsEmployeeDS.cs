using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class HRProvisionsEmployeeDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public List<HrProvisionsEmployeeVw>? Details { get; set; }
  }
}
