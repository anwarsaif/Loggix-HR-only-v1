using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrVisitScheduleLocationDto
    {
        public long Id { get; set; }
        public long? VisitScheduleId { get; set; }
        public long? EmpId { get; set; }
        public long? CustomerId { get; set; }
        public long? LocationId { get; set; }
        public long? ShiftId { get; set; }
        [StringLength(10)]
        public string? StartTime { get; set; }
        [StringLength(10)]
        public string? EndTime { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string? Summary { get; set; }
        public int? StatusId { get; set; }
        public long? AppId { get; set; }
        public long? StepId { get; set; }
        public string? LastNote { get; set; }
        public bool? CurLongitude { get; set; }
        public bool? CurLatitude { get; set; }
        public string? FileUrl { get; set; }
    }

    public class HrVisitScheduleLocationEditDto
    {
        public long Id { get; set; }
        public long? VisitScheduleId { get; set; }
        public long? EmpId { get; set; }
        public long? CustomerId { get; set; }
        public long? LocationId { get; set; }
        public long? ShiftId { get; set; }
        [StringLength(10)]
        public string? StartTime { get; set; }
        [StringLength(10)]
        public string? EndTime { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string? Summary { get; set; }
        public int? StatusId { get; set; }
        public long? AppId { get; set; }
        public long? StepId { get; set; }
        public string? LastNote { get; set; }
        public bool? CurLongitude { get; set; }
        public bool? CurLatitude { get; set; }
        public string? FileUrl { get; set; }
    }
}
