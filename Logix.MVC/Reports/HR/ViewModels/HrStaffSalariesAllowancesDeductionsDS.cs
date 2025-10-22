using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class HrStaffSalariesAllowancesDeductionsDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public HrStaffSalariesAllowancesDeductionsFilterDto? Filter { get; set; }
    public List<HrStaffSalariesAllowancesDeductionsFilterDto>? Details { get; set; }
  }
}
