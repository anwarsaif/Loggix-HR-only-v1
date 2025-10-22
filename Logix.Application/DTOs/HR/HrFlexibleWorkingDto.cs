using Logix.Domain.HR;

namespace Logix.Application.DTOs.HR
{
    public class HrFlexibleWorkingDto
    {
        public long? Id { get; set; }
        public long? MasterId { get; set; }
        public long? EmpId { get; set; }
        public string? AttendanceDate { get; set; }
        public string? TotalHours { get; set; }
        public long? Minute { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StatusId { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public long? ActualMinute { get; set; }
    }
    public class HrFlexibleWorkingEditDto
    {
        public long? Id { get; set; }
        public long? MasterId { get; set; }
        public long? EmpId { get; set; }
        public string? AttendanceDate { get; set; }
        public string? TotalHours { get; set; }
        public long? Minute { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StatusId { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public long? ActualMinute { get; set; }
    }
    public class HrFlexibleWorkingResultDto : HrAttendancesVw
    {

        public int? ActualMinute { get; set; }
        public string? ActualHours { get; set; }
        public string? TimeInString { get; set; }
        public string? TimeOutString { get; set; }
        public int? Minutes { get; set; }
        public long? Id { get; set; }
        public string? StringMinutes { get; set; }
    }
}
