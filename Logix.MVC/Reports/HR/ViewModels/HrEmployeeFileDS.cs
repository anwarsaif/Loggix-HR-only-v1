using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class HrEmployeeFileDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public HrEmployeeFileFilterDto? Filter { get; set; }
    public List<HrEmployeeFileFilterDto>? Details { get; set; }
  }
}
