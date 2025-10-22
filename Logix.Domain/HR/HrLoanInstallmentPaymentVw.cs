using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrLoanInstallmentPaymentVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Loan_Installment_ID")]
        public long? LoanInstallmentId { get; set; }
        [Column("Payroll_ID")]
        public long? PayrollId { get; set; }
        [Column("Payroll_D_ID")]
        public long? PayrollDId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Loan_Payment_ID")]
        public long? LoanPaymentId { get; set; }
        [Column("Amount_Paid", TypeName = "decimal(18, 2)")]
        public decimal? AmountPaid { get; set; }
        [Column("Intallment_No")]
        public int? IntallmentNo { get; set; }
        [Column("Loan_ID")]
        public long? LoanId { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
    }
}
