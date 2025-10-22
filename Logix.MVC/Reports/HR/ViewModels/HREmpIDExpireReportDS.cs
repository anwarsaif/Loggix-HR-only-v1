using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class HREmpIDExpireReportDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public HREmpIDExpireReportFilterDto? Filter { get; set; }
    public List<HREmpIDExpireReportFilterDto>? Details { get; set; }
  }
}
