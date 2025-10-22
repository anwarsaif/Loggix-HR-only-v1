using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class ReportOhadDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public HRRPOhadFilterDto? Filter { get; set; }
    public List<HRRPOhadFilterDto>? Details { get; set; }
  }
}
