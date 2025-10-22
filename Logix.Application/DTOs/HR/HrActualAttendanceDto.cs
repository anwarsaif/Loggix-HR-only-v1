using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public partial class HrActualAttendanceDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public DateTime? Checktimein { get; set; }
        public DateTime? Checktimeout { get; set; }
        public string? Date { get; set; }
        public int? DayNo { get; set; }
    }

    public partial class HrActualAttendanceReportDto
    {
        public long? EmpId { get; set; }
        public string? Checktimein { get; set; }
        public string? Date { get; set; }
        public int? DayNo { get; set; }
        public string? DayName { get; set; }
        public string? TotalTime { get; set; }
        public string? CHECKTIMEOut2 { get; set; }
        public string? CHECKTIMEIN2 { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set; }
        public int? TotalMinutes { get; set; }
        public int? TotalSecondIN { get; set; }
        ///////////////
        public string? EmpName { get; set; }
        public string? BraName { get; set; }
        public string? DepName { get; set; }

        public string? EmpCode { get; set; }
        public string? FormattedExitTime { get; set; }
        public string? FormattedWorkTime { get; set; }
        public string? FormattedTotalTime { get; set; }
    }

}
