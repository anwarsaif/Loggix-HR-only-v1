using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_ActualAttendance", Schema = "dbo")]
    public partial class HrActualAttendance
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("CHECKTIMEIN", TypeName = "datetime")]
        public DateTime? Checktimein { get; set; }
        [Column("CHECKTIMEOut", TypeName = "datetime")]
        public DateTime? Checktimeout { get; set; }
        [StringLength(10)]
        public string? Date { get; set; }
        [Column("Day_No")]
        public int? DayNo { get; set; }
    }
}
