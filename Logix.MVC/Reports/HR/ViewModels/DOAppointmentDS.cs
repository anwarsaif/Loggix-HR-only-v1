using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class DOAppointmentDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public DOAppointmentFilterDto? Filter { get; set; }
    public List<DOAppointmentFilterDto>? Details { get; set; }
  }
}
