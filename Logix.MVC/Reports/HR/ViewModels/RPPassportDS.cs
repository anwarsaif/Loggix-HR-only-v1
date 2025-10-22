using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class RPPassportDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public RPPassportFilterDto? Filter { get; set; }
    public List<RPPassportFilterDto>? Details { get; set; }
  }
}
