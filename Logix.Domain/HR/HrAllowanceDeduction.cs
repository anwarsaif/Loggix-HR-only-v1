using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Allowance_Deduction", Schema = "dbo")]
    [Index("EmpId", "TypeId", "AdId", "Amount", "StartDate", "EndDate", "FixedOrTemporary", "DueDate", Name = "Ind_Emp_ID")]
    [Index("IsDeleted", Name = "Ind_Isdel")]
    public partial class HrAllowanceDeduction
    {
        [Key]
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
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Status { get; set; }
        [Column("Fixed_Or_Temporary")]
        public int? FixedOrTemporary { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
        [Column("Preparation_Salaries_ID")]
        public long? PreparationSalariesId { get; set; }
        [Column("M_AD_ID")]
        public long? MAdId { get; set; }
    }
}
