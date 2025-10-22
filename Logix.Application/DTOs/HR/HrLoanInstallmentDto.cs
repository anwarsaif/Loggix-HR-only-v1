namespace Logix.Application.DTOs.HR
{
    public class HrLoanInstallmentDto
    {
        public long Id { get; set; }
        public long? LoanId { get; set; }
        public int? IntallmentNo { get; set; }
        public decimal? Amount { get; set; }
        public string? DueDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPaid { get; set; }
        public string? RecepitNo { get; set; }
        public string? RecepitDate { get; set; }
        public decimal? Rate { get; set; }
    }
    public class HrLoanInstallmentEditDto
    {
        public long Id { get; set; }
        public long? LoanId { get; set; }
        public int? IntallmentNo { get; set; }
        public decimal? Amount { get; set; }
        public string? DueDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsPaid { get; set; }
        public string? RecepitNo { get; set; }
        public string? RecepitDate { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class HrLoanInstallmentSummaryDto
    {
        public int? IntallmentNo { get; set; }
        public string? DueDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? SameMonthInstallments { get; set; }
        public decimal? Rate { get; set; }
    }

    public class HrLoanInstallmentInputDto
    {
        public string? empCode { get; set; }
        public string? fromDate { get; set; }
        public decimal? installmentcount { get; set; }
        public decimal? installmentValue { get; set; }
        public long? LoanId { get; set; }
    }

    public class HrLoanInstallmentInput2Dto
    {
        public string? empCode { get; set; }
        public string? fromDate { get; set; }
        public decimal? installmentValue { get; set; }
    }

    public class HrLoanInstallmentNotLoanIdDto
    {
        public long? empId { get; set; }
        public string? fromDate { get; set; }
        public long? LoanId { get; set; }
    }
}
