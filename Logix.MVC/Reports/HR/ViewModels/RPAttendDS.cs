using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class RPAttendDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public RPAttendFilterDto? Filter { get; set; }
    public List<RPAttendFilterDto>? Details { get; set; }
  }
}
