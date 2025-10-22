using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Increments")]
    public partial class HrIncrement : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Increase_Date")]
        [StringLength(10)]
        public string? IncreaseDate { get; set; }
        [Column("Increase_Amount", TypeName = "decimal(18, 2)")]
        public decimal? IncreaseAmount { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Allowances { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deductions { get; set; }
        [Column("Status_ID")]
        public bool? StatusId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NewSalary { get; set; }
        [Column("Apply_Type")]
        public long? ApplyType { get; set; }
        public string? Note { get; set; }
        [Column("Cur_Cat_Job_ID")]
        public long? CurCatJobId { get; set; }
        [Column("Cur_Job_ID")]
        public long? CurJobId { get; set; }
        [Column("Cur_Level_ID")]
        public long? CurLevelId { get; set; }
        [Column("Cur_Grad_ID")]
        public long? CurGradId { get; set; }
        [Column("New_Level_ID")]
        public long? NewLevelId { get; set; }
        [Column("New_Grad_ID")]
        public long? NewGradId { get; set; }
        [Column("New_Cat_Job_ID")]
        public long? NewCatJobId { get; set; }
        [Column("New_Job_ID")]
        public long? NewJobId { get; set; }
        [Column("Decision_No")]
        [StringLength(250)]
        public string? DecisionNo { get; set; }
        [Column("Decision_Date")]
        [StringLength(10)]
        public string? DecisionDate { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        [Column("App_ID")]
        public int? AppId { get; set; }
        [Column("Package_Number")]
        public long? PackageNumber { get; set; }
    }
}
