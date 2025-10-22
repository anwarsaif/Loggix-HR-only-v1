using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Direct_Job")]
    public partial class HrDirectJob : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("date1")]
        public string? Date1 { get; set; }
        [Column("Date_Direct")]
        [StringLength(10)]
        public string? DateDirect { get; set; }
        public string? Note { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Vacation_ID")]
        public long? VacationId { get; set; }
    }
}
