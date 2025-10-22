using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class RPContractDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public HrRPContractFilterDto? Filter { get; set; }
    public List<HrRPContractFilterDto>? Details { get; set; }
  }
}
