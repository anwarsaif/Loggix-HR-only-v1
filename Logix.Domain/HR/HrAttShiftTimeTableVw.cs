using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrAttShiftTimeTableVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Shift_ID")]
        public long? ShiftId { get; set; }
        [Column("TimeTable_ID")]
        public long? TimeTableId { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Shift_Name")]
        [StringLength(50)]
        public string? ShiftName { get; set; }
        [Column("TimeTable_Name")]
        [StringLength(50)]
        public string? TimeTableName { get; set; }
        [Column("On_Duty_Time", TypeName = "time(5)")]
        public TimeSpan? OnDutyTime { get; set; }
        [Column("Off_Duty_Time", TypeName = "time(5)")]
        public TimeSpan? OffDutyTime { get; set; }
    }
}
