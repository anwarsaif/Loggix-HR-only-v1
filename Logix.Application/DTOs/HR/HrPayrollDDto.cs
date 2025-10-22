using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrPayrollDDto
    {
        public long MsdId { get; set; }
        public long MsId { get; set; }
        [StringLength(10)]
        public string? JobId { get; set; }
        public long? EmpId { get; set; }
        public decimal? Salary { get; set; }
        [StringLength(50)]
        public string? EmpAccountNo { get; set; }
        public int? BankId { get; set; }
        public int? CountDayWork { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? TransactionInstallment { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? Commission { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? Mandate { get; set; }
        public decimal? HOverTime { get; set; }
        public decimal? HDelay { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? SalaryOrignal { get; set; }
        public long? RefranceNo { get; set; }
        public long? DeptId { get; set; }
        public long? LocationId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public long? SalaryGroupId { get; set; }
        public long? CcId { get; set; }
        public int? CntAbsence { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? WagesProtection { get; set; }
        public decimal? AllowanceOrignal { get; set; }
        public decimal? DeductionOrignal { get; set; }
        [StringLength(50)]
        public string? Iban { get; set; }
    }
    public class HrPayrollDEditDto
    {
        public long MsdId { get; set; }
        public long MsId { get; set; }
        [StringLength(10)]
        public string? JobId { get; set; }
        public long? EmpId { get; set; }
        public decimal? Salary { get; set; }
        [StringLength(50)]
        public string? EmpAccountNo { get; set; }
        public int? BankId { get; set; }
        public int? CountDayWork { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? TransactionInstallment { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? Commission { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? Mandate { get; set; }
        public decimal? HOverTime { get; set; }
        public decimal? HDelay { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? SalaryOrignal { get; set; }
        public long? RefranceNo { get; set; }
        public long? DeptId { get; set; }
        public long? LocationId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public long? SalaryGroupId { get; set; }
        public long? CcId { get; set; }
        public int? CntAbsence { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? WagesProtection { get; set; }
        public decimal? AllowanceOrignal { get; set; }
        public decimal? DeductionOrignal { get; set; }
        [StringLength(50)]
        public string? Iban { get; set; }
    }
    public class HRPayrollDStoredProcedureDto
    {
        public long? MS_ID { get; set; }
        public long? MSD_ID { get; set; }
        public int? Job_ID { get; set; }
        public long? Emp_ID { get; set; }
        public decimal? Salary { get; set; }
        public string? Emp_Account_No { get; set; }
        public int? BankId { get; set; }
        public int? Count_Day_Work { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? Transaction_Installment { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CMDTYPE { get; set; }
        public int? Dept_ID { get; set; }
        public int? Location { get; set; }
        public int? BRANCH_ID { get; set; }
        public int? FinancelYear { get; set; }
        public int? Facility_ID { get; set; }
        public int? Fin_year { get; set; }
        public string? Emp_Code { get; set; }
        public string ?Emp_name { get; set; }
        public int? Sponsors_ID { get; set; }
        public decimal? Commission { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? Mandate { get; set; }
        public decimal? H_OverTime { get; set; }
        public decimal? Penalties { get; set; }
        public int? Nationality_ID { get; set; }
        public decimal? Salary_Orignal { get; set; }
        public int? Payment_Type_ID { get; set; }
        public int? Wages_Protection { get; set; }
        public long? Refrance_No { get; set; }
        public long? MS_Month { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? Salary_Group_ID { get; set; }
        public int? Cnt_Absence { get; set; }
        public decimal? H_Delay { get; set; }
        public string ?BRANCHS_ID { get; set; }
        public decimal? IncomeTax { get; set; }
        public string ?AmountWrite { get; set; }
        public decimal? DelayHourByDay { get; set; }
    }

    public class PayrollAccountingEntryDto
    {
        public long? AccountID { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public long? ReferenceNo { get; set; } 
        public int ReferenceTypeID { get; set; }
        public long? CCId { get; set; }
        public string? Description { get; set; }
        public string? EmpName { get; set; }
        public long? EmpId { get; set; }
    }
    public class PayrollAccountingEntryResultDto
    {
        public string? LocationName { get; set; }
        public string? DeptName { get; set; }
        public long? DeptId { get; set; }
        public long? BranchId { get; set; }
        public string? BranchName { get; set; }

        public int? CntEmp { get; set; }
        public decimal? TotalBasicSalary { get; set; }
        public decimal ?TotalFixedAllowance { get; set; }
        public decimal? TotalOtherAllowance { get; set; }
        public decimal? TotalOverTime { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? TotalLoan { get; set; }
        public decimal ?TotalOtherDeduction { get; set; }
        public decimal? TotalNet { get; set; }
    }



}
