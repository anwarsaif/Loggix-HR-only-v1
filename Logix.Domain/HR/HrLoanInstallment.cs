using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;

namespace Logix.Domain.HR
{
    [Table("HR_Loan_Installment")]
    public partial class HrLoanInstallment : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Loan_ID")]
        public long? LoanId { get; set; }
        [Column("Intallment_No")]
        public int? IntallmentNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
        public bool? IsPaid { get; set; }
        [Column("Recepit_No")]
        [StringLength(50)]
        public string? RecepitNo { get; set; }
        [Column("Recepit_Date")]
        [StringLength(10)]
        public string? RecepitDate { get; set; }
    }
}
