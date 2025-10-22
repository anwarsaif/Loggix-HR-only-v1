using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;

namespace Logix.Domain.HR
{
    [Table("HR_Loan_Installment_Payment")]
    public partial class HrLoanInstallmentPayment : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Loan_Installment_ID")]
        public long? LoanInstallmentId { get; set; }
        [Column("Payroll_ID")]
        public long? PayrollId { get; set; }
        [Column("Payroll_D_ID")]
        public long? PayrollDId { get; set; }
        [Column("Loan_Payment_ID")]
        public long? LoanPaymentId { get; set; }
        [Column("Amount_Paid", TypeName = "decimal(18, 2)")]
        public decimal? AmountPaid { get; set; }
    }
}
