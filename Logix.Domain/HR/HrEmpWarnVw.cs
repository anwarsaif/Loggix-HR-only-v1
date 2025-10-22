using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrEmpWarnVw : TraceEntity
    {
        public long EmpWarnId { get; set; }
        public int? WarnWhy { get; set; }
        [StringLength(50)]
        public string? WarnDate { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [StringLength(1500)]
        public string? Note { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("AD_ID")]
        public int? AdId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Deducation_Days")]
        public int? DeducationDays { get; set; }
        [Column("Name_Deduction")]
        [StringLength(250)]
        public string? NameDeduction { get; set; }
        public int? Expr2 { get; set; }
    }
}
