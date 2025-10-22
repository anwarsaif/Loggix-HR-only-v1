using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class RPBankDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public RPBankFilterDto? Filter { get; set; }
    public List<RPBankFilterDto>? Details { get; set; }
  }
}
