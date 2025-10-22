using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrAttendancesVw
    {
        [Column("Attendance_Id")]
        public long AttendanceId { get; set; }
        [Column("Time_In", TypeName = "datetime")]
        public DateTime? TimeIn { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
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
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Def_Time_In", TypeName = "datetime")]
        public DateTime? DefTimeIn { get; set; }
        [Column("Def_Time_Out", TypeName = "datetime")]
        public DateTime? DefTimeOut { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Day_Date_Hijri")]
        [StringLength(10)]
        public string? DayDateHijri { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        public string? WhyException { get; set; }
        public string? Latitude { get; set; }
        [Column("longitude")]
        public string? Longitude { get; set; }
        [Column("TimeTable_ID")]
        public long? TimeTableId { get; set; }
        [Column("Day_Date_Gregorian")]
        [StringLength(10)]
        public string? DayDateGregorian { get; set; }
        [StringLength(50)]
        public string? LogInBy { get; set; }
        [StringLength(50)]
        public string? LogOutBy { get; set; }
        public bool? IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        [Column("TimeTable_Name")]
        [StringLength(50)]
        public string? TimeTableName { get; set; }
        [Column("Day_Name")]
        [StringLength(50)]
        public string? DayName { get; set; }
        [Column("Day_Name2")]
        [StringLength(50)]
        public string? DayName2 { get; set; }
        [Column("Attendance_Type_Name")]
        [StringLength(250)]
        public string? AttendanceTypeName { get; set; }
        [Column("Manager_ID")]
        public long? ManagerId { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Late_Time_M")]
        public int? LateTimeM { get; set; }
        [Column("Leave_Early_Time_M")]
        public int? LeaveEarlyTimeM { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Sponsors_ID")]
        public int? SponsorsId { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Bank_Name2")]
        [StringLength(250)]
        public string? BankName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Marital_Status_Name2")]
        [StringLength(250)]
        public string? MaritalStatusName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
        public decimal? DailyWorkingHours { get; set; }
        [Column("Manager3_ID")]
        public long? Manager3Id { get; set; }
        [Column("Manager2_ID")]
        public long? Manager2Id { get; set; }
        [Column("Attendance_Type_Name2")]
        [StringLength(250)]
        public string? AttendanceTypeName2 { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Payroll_Type")]
        public int? PayrollType { get; set; }
        [Column("Hour_Cost", TypeName = "decimal(18, 2)")]
        public decimal? HourCost { get; set; }
        [Column("On_Duty_Time", TypeName = "time(5)")]
        public TimeSpan? OnDutyTime { get; set; }
        [Column("Off_Duty_Time", TypeName = "time(5)")]
        public TimeSpan? OffDutyTime { get; set; }
    }
}
