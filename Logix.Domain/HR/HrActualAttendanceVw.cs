using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrActualAttendanceVw
    {
        [Column("CHECKTIMEIN", TypeName = "datetime")]
        public DateTime? Checktimein { get; set; }
        [Column("CHECKTIMEOut", TypeName = "datetime")]
        public DateTime? Checktimeout { get; set; }
        [StringLength(10)]
        public string? Date { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Day_Name")]
        [StringLength(50)]
        public string? DayName { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
    }
}
