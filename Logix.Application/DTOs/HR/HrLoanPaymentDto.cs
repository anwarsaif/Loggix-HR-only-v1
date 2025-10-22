using Logix.Application.DTOs.Main;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrLoanPaymentDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? PayDate { get; set; }
        public string? VoucherNo { get; set; }
        public string? VoucherDate { get; set; }
        public decimal? PayAmount { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class HrLoanPaymentEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? PayDate { get; set; }
        public string? VoucherNo { get; set; }
        public string? VoucherDate { get; set; }
        public decimal? PayAmount { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class HrLoanPaymentFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? VoucherNo { get; set; }
        public decimal? PayAmount { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }


    }
    public class HrLoanPaymentAddDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public string? PayDate { get; set; }
        public string? VoucherNo { get; set; }
        public string? VoucherDate { get; set; }
        public decimal? PayAmount { get; set; }
        public string? Note { get; set; }
        public List<HrLoanInstallmentPaymentAddDto>? Details { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }

    public class HrLoanInstallmentPaymentAddDto
    {
        [Required]
        public decimal Amount { get; set; }
        public decimal NewAmount { get; set; }
        [Required]

        public long? LoanInstallmentId { get; set; }

    }
}
