using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Attendance_BioTime")]

    public class HrAttendanceBioTime
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("EmpID")]
        [StringLength(10)]
        public string? EmpId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Checktime { get; set; }
        public int? Verified { get; set; }
        public int? CheckState { get; set; }
        public int? WorkCode { get; set; }
        [Column("BranchID")]
        public int? BranchId { get; set; }
        [Column("send_logix")]
        public int? SendLogix { get; set; }
        public int? Repeater { get; set; }
        [StringLength(10)]
        public string? Date { get; set; }
    }
}

