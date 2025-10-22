using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Payroll_allowance_Deduction")]
    [Index("AdId", "Debit", "Credit", "MsId", Name = "AD_indeix")]
    public partial class HrPayrollAllowanceDeduction
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("MS_ID")]
        public long? MsId { get; set; }
        [Column("MSD_ID")]
        public long? MsdId { get; set; }
        [Column("AD_ID")]
        public long? AdId { get; set; }
        [Column("AD_Value", TypeName = "decimal(18, 2)")]
        public decimal? AdValue { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Credit { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("AD_Value_Orignal", TypeName = "decimal(18, 2)")]
        public decimal? AdValueOrignal { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Fixed_Or_Temporary")]
        public int? FixedOrTemporary { get; set; }
    }
}
