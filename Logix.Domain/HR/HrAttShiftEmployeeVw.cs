using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrAttShiftEmployeeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Shit_ID")]
        public long? ShitId { get; set; }
        [Column("Begin_Date")]
        [StringLength(10)]
        public string? BeginDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("Shift_ID")]
        public long? ShiftId { get; set; }
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
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesId { get; set; }
        [Column("Day_No")]
        public int? DayNo { get; set; }
        [Column("TimeTable_ID")]
        public long? TimeTableId { get; set; }
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
        public bool? EntryPreviousDay { get; set; }
        public long? CheckoutTimeAllowed { get; set; }
    }
}
