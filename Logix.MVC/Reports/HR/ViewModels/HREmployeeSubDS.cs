
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
    public class HREmployeeSubDS
    {
        public ReportBasicDataDto? BasicData { get; set; }
        public EmployeeSubFilterDto? Filter { get; set; }
        public List<HrEmployeeVw>? Details { get; set; }
    }
}
