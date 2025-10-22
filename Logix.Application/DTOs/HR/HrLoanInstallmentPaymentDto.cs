namespace Logix.Application.DTOs.HR
{
    public class HrLoanInstallmentPaymentDto
    {
        public long Id { get; set; }
        public long? LoanInstallmentId { get; set; }
        public long? PayrollId { get; set; }
        public long? PayrollDId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public long? LoanPaymentId { get; set; }
        public decimal? AmountPaid { get; set; }
    }

    public class HrLoanInstallmentPaymentEditDto
    {
        public long Id { get; set; }
        public long? LoanInstallmentId { get; set; }
        public long? PayrollId { get; set; }
        public long? PayrollDId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? LoanPaymentId { get; set; }
        public decimal? AmountPaid { get; set; }
    }

}
