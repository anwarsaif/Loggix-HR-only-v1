using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Logix.Domain.Base;

namespace Logix.Domain.HR
{
    [Table("HR_Delay")]
    [Index("EmpId", "DelayDate", Name = "Ind_Emp_ID")]
    [Index("IsDeleted", Name = "Ind_Isdel")]
    public partial class HrDelay:TraceEntity
    {
        [Key]
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
      
        [Column("Attendance_Id")]
        public long? AttendanceId { get; set; }
        public string? Note { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
    }
}
