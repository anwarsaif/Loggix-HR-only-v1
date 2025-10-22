using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Flexible_Working", Schema = "dbo")]
    public partial class HrFlexibleWorking
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Master_ID")]
        public long? MasterId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
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
        [Column("Time_In", TypeName = "datetime")]
        public DateTime? TimeIn { get; set; }
        [Column("Time_Out", TypeName = "datetime")]
        public DateTime? TimeOut { get; set; }
        public long? ActualMinute { get; set; }
    }
}
