using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrFlexibleWorkingVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Master_ID")]
        public long? MasterId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Attendance_Date")]
        [StringLength(10)]
        public string? AttendanceDate { get; set; }
        [StringLength(50)]
        public string? TotalHours { get; set; }
        public long? Minute { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalPrice { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
        public decimal? DailyWorkingHours { get; set; }
        [Column("Time_In", TypeName = "datetime")]
        public DateTime? TimeIn { get; set; }
        [Column("Time_Out", TypeName = "datetime")]
        public DateTime? TimeOut { get; set; }
        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }
        [Column("Bank_ID")]
        public int? BankId { get; set; }
        public long? ActualMinute { get; set; }
    }
}
