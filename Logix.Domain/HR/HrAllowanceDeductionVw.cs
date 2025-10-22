using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrAllowanceDeductionVw:TraceEntity
    {
        [StringLength(250)]
        public string? Name { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("AD_ID")]
        public int? AdId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Decision_No")]
        [StringLength(50)]
        public string? DecisionNo { get; set; }
        [Column("Decision_Date")]
        [StringLength(50)]
        public string? DecisionDate { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [StringLength(1500)]
        public string? Note { get; set; }
        public int? FinancelYear { get; set; }
        public bool? Status { get; set; }
        [Column("Fixed_Or_Temporary")]
        public int? FixedOrTemporary { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [StringLength(250)]
        public string? Name2 { get; set; }
    }
}
