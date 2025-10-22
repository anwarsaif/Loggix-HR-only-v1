using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class RPMedicalInsuranceDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public RPMedicalInsuranceFilterDto? Filter { get; set; }
    public List<RPMedicalInsuranceFilterDto>? Details { get; set; }
  }
}
