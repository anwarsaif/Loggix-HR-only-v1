using Logix.Application.DTOs.Main;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrLoanDto
    {
        public long Id { get; set; }
        [Required]
        [StringLength(10)]
        public string? LoanDate { get; set; }
        [Required]
        public decimal? LoanValue { get; set; }
        public decimal? InstallmentValue { get; set; }
        public int? InstallmentCount { get; set; }
        public int? InstallmentCountPaid { get; set; }
        public int? InstallmentCountRemaining { get; set; }
        public decimal? InstallmentLastValue { get; set; }
        [StringLength(10)]
        public string? StartDatePayment { get; set; }
        [StringLength(50)]
        public string? EndDatePayment { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        [Required]
        public string? EmpId { get; set; }
        public string? Note { get; set; }
        [Required]
        public int? Type { get; set; }
        public bool? CreateInstallment { get; set; }
        public long? Guarantor1EmpId { get; set; }
        //[StringLength(10)]
        //public string? Guarantor2EmpId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<HrLoanInstallmentDto>? Details { get; set; }
        public List<SaveFileDto>? files { get; set; }

    }
    public class HrLoan4Dto
    {
        public long Id { get; set; }
        [StringLength(10)]
        public string? LoanDate { get; set; }
        public decimal? LoanValue { get; set; }
        public decimal? InstallmentValue { get; set; }
        public int? InstallmentCount { get; set; }
        public int? InstallmentCountPaid { get; set; }
        public int? InstallmentCountRemaining { get; set; }
        public decimal? InstallmentLastValue { get; set; }
        [StringLength(10)]
        public string? StartDatePayment { get; set; }
        [StringLength(50)]
        public string? EndDatePayment { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string? EmpId { get; set; }
        public string? Note { get; set; }
        public int? Type { get; set; }
        public bool? CreateInstallment { get; set; }
        public long? Guarantor1EmpId { get; set; }
        //[StringLength(10)]
        //public string? Guarantor2EmpId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<SaveFileDto>? files { get; set; }

    }

    public class HrLoanEditDto
    {
        public long Id { get; set; }
        [StringLength(10)]
        public string? LoanDate { get; set; }
        public decimal? LoanValue { get; set; }
        public decimal? InstallmentValue { get; set; }
        public int? InstallmentCount { get; set; }
        public int? InstallmentCountPaid { get; set; }
        public int? InstallmentCountRemaining { get; set; }
        public decimal? InstallmentLastValue { get; set; }
        [StringLength(10)]
        public string? StartDatePayment { get; set; }
        [StringLength(50)]
        public string? EndDatePayment { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string? EmpId { get; set; }
        public string? Note { get; set; }
        public int? Type { get; set; }
        public bool? CreateInstallment { get; set; }
        public long? Guarantor1EmpId { get; set; }
        //[StringLength(10)]
        //public string? Guarantor2EmpId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }










    public partial class HrLoanFilterDto
    {
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? EmpCode { get; set; }
        public int? Type { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        /// <summary>
        /// //////////////////////Result///////////////
        /// </summary>
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }

        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }
        public string? DepName { get; set; }
        public string? DepName2 { get; set; }
        //  المبلغ
        public decimal? LoanValue { get; set; }
        public string? LoanDate { get; set; }
        public decimal? RemainingAmount { get; set; }
        public decimal? InstallmentValue { get; set; }
        public int? InstallmentCount { get; set; }
        public string? EndDatePayment { get; set; }
        public string? StartDatePayment { get; set; }
        public string? Note { get; set; }
        public long? Id { get; set; }
        public int? LoanStatus { get; set; }
        public int? LoanNature { get; set; }




    }

    public class HrEditLoanDto
    {
        public long Id { get; set; }
        [StringLength(10)]
        public string? LoanDate { get; set; }
        [Required]
        public decimal? LoanValue { get; set; }
        [Required]
        public decimal? InstallmentValue { get; set; }
        public int? InstallmentCount { get; set; }
        public int? InstallmentCountPaid { get; set; }
        public int? InstallmentCountRemaining { get; set; }
        public decimal? InstallmentLastValue { get; set; }
        [Required]
        [StringLength(10)]
        public string? StartDatePayment { get; set; }
        [Required]
        [StringLength(50)]
        public string? EndDatePayment { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string? EmpId { get; set; }
        public string? Note { get; set; }
        public int? Type { get; set; }
        public bool? CreateInstallment { get; set; }
        public long? Guarantor1EmpId { get; set; }
        //[StringLength(10)]
        //public string? Guarantor2EmpId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public List<HrLoanInstallmentEditDto>? Details { get; set; }
        public List<SaveFileDto>? files { get; set; }

    }
    public class InstallmentCountChangedDto
    {
        public string empCode { get; set; } = null!;
        public int Installmentcount { get; set; } = 0!;
        public string SDatePyment { get; set; } = null!;
        public decimal LoanValue { get; set; } = 0!;
    }
    public class InstallmentValueChangedDto
    {
        public int InstallmentValue { get; set; } = 0!;
        public string StartDatePayment { get; set; } = null!;
        public decimal LoanValue { get; set; } = 0!;
    }
    public class InstallmentScheduleDto
    {
        public long LoanId { get; set; } = 0!;
        public string empCode { get; set; } = null!;
        public int Installmentcount { get; set; } = 0!;
        public string SDatePyment { get; set; } = null!;

    }
}
