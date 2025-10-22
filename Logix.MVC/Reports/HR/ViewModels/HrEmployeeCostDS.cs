using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class HrEmployeeCostDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public HrEmployeeCostFilterDto? Filter { get; set; }
    public List<HrEmployeeCostFilterDto>? Details { get; set; }
  }
}
