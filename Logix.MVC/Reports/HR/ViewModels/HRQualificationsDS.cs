
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
    public class HRQualificationsDS
    {
        public ReportBasicDataDto? BasicData { get; set; }
        public HrQualificationsFilterDto? Filter { get; set; }
        public List<HrEmployeeVw>? Details { get; set; }
    }
}
