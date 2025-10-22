using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_CheckInOut")]

    public class HrCheckInOut
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("CHECKTIME", TypeName = "datetime")]
        public DateTime? Checktime { get; set; }
        [Column("CHECKTYPE")]
        public int? Checktype { get; set; }
        [Column("Day_No")]
        public int? DayNo { get; set; }
        public bool? IsSend { get; set; }
        [Column("Send_ActualAttendance")]
        public bool? SendActualAttendance { get; set; }
    }
}
