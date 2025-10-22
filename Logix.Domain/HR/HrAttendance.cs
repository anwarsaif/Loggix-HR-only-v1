using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Attendances")]
    public partial class HrAttendance : TraceEntity
    {
        [Key]
        [Column("Attendance_Id")]
        public long AttendanceId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [Column("Time_In", TypeName = "datetime")]
        public DateTime? TimeIn { get; set; }
        [Column("Note_In")]
        [StringLength(500)]
        public string? NoteIn { get; set; }
        [Column("Time_Out", TypeName = "datetime")]
        public DateTime? TimeOut { get; set; }
        [Column("Note_Out")]
        [StringLength(500)]
        public string? NoteOut { get; set; }
        [Column("Att_Type")]
        public int? AttType { get; set; }
        [Column("Day_No")]
        public int? DayNo { get; set; }
        [Column("AllowTime_In")]
        public bool? AllowTimeIn { get; set; }
        [Column("AllowTime_Out")]
        public bool? AllowTimeOut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Column("Def_Time_In", TypeName = "datetime")]
        public DateTime? DefTimeIn { get; set; }
        [Column("Def_Time_Out", TypeName = "datetime")]
        public DateTime? DefTimeOut { get; set; }
        [Column("Day_Date_Gregorian")]
        [StringLength(10)]
        public string? DayDateGregorian { get; set; }
        [Column("Day_Date_Hijri")]
        [StringLength(10)]
        public string? DayDateHijri { get; set; }

        public string? WhyException { get; set; }
        [StringLength(50)]
        public string? LogInBy { get; set; }
        [StringLength(50)]
        public string? LogOutBy { get; set; }
        [Column("TimeTable_ID")]
        public long? TimeTableId { get; set; }
        [Column("longitude")]
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("longitude_out")]
        public string? LongitudeOut { get; set; }
        [Column("Latitude_out")]
        public string? LatitudeOut { get; set; }
        [Column("ISNextDay")]
        public bool? IsnextDay { get; set; }
    }
}
