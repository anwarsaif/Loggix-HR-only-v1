using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Att_TimeTable")]
    public partial class HrAttTimeTable : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("TimeTable_Name")]
        [StringLength(50)]
        public string? TimeTableName { get; set; }
        [Column("On_Duty_Time", TypeName = "time(5)")]
        public TimeSpan? OnDutyTime { get; set; }
        [Column("Off_Duty_Time", TypeName = "time(5)")]
        public TimeSpan? OffDutyTime { get; set; }
        [Column("Late_Time_M")]
        public int? LateTimeM { get; set; }
        [Column("Leave_Early_Time_M")]
        public int? LeaveEarlyTimeM { get; set; }
        [Column("Begin_in", TypeName = "time(5)")]
        public TimeSpan? BeginIn { get; set; }
        [Column("End_In", TypeName = "time(5)")]
        public TimeSpan? EndIn { get; set; }
        [Column("Begin_Out", TypeName = "time(5)")]
        public TimeSpan? BeginOut { get; set; }
        [Column("End_Out", TypeName = "time(5)")]
        public TimeSpan? EndOut { get; set; }
        [Column("Flexible_Attendance")]
        public bool? FlexibleAttendance { get; set; }
        [Column("Flexible_Start", TypeName = "time(5)")]
        public TimeSpan? FlexibleStart { get; set; }
        [Column("Flexible_END", TypeName = "time(5)")]
        public TimeSpan? FlexibleEnd { get; set; }
        public bool? ExitOnNextDate { get; set; }
        public bool? Overtime { get; set; }
        [Column("IS24HourShift")]
        public bool? Is24hourShift { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? ShiftWorkHour { get; set; }
        public long? CheckoutTimeAllowed { get; set; }
        public bool? EntryPreviousDay { get; set; }
    }
}
