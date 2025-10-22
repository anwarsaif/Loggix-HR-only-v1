using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_PS_Allowance_Deduction")]
    public partial class HrPsAllowanceDeduction
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("PS_ID")]
        public long? PsId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Credit { get; set; }
        [Column("Amount_Orginal", TypeName = "decimal(18, 2)")]
        public decimal? AmountOrginal { get; set; }
        [Column("AD_ID")]
        public int? AdId { get; set; }
        [Column("Fixed_Or_Temporary")]
        public int? FixedOrTemporary { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
