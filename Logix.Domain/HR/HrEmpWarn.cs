using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Emp_Warn")]
    public partial class HrEmpWarn : TraceEntity
    {
        [Key]
        public long EmpWarnId { get; set; }
        public int? WarnWhy { get; set; }
        [StringLength(50)]
        public string? WarnDate { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [StringLength(1500)]
        public string? Note { get; set; }
        [Column("AD_ID")]
        public int? AdId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Deducation_Days")]
        public int? DeducationDays { get; set; }
    }
}
