using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrDelayVw
    {
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Delay_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DelayDate { get; set; }
        [Column("Delay_Time")]
        public TimeSpan? DelayTime { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? Expr1 { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Attendance_Id")]
        public long? AttendanceId { get; set; }
        public string? Note { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
        public decimal? DailyWorkingHours { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
    }
}
