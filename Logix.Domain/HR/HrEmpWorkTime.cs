using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Emp_Work_Time")]
    public partial class HrEmpWorkTime:TraceEntity
    {
        [Key]
        public long EmpWorkId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [Column("In_AM")]
        [StringLength(50)]
        public string? InAm { get; set; }
        [Column("Out_AM")]
        [StringLength(50)]
        public string? OutAm { get; set; }
        [Column("In_PM")]
        [StringLength(50)]
        public string? InPm { get; set; }
        [Column("Out_PM")]
        [StringLength(50)]
        public string? OutPm { get; set; }
        [Column("In_PM_Yest")]
        [StringLength(50)]
        public string? InPmYest { get; set; }
        [Column("Out_PM_Yest")]
        [StringLength(50)]
        public string? OutPmYest { get; set; }
        [StringLength(10)]
        public string? FromDate { get; set; }
        [StringLength(10)]
        public string? ToDate { get; set; }
    }
}
