using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Att_Location_Employee")]

    public class HrAttLocationEmployee : TraceEntity
    {
       
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Begin_Date")]
        [StringLength(10)]
        public string? BeginDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }



    }
}
