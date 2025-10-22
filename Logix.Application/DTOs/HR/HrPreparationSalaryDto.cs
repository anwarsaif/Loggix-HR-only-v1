using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{
    public class HrPreparationSalaryDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }

        public long? DeptId { get; set; }
        public long? Location { get; set; }
        public int? FinancelYear { get; set; }
        public string? MsMonth { get; set; }
        public string? MsDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public int? CountDayWork { get; set; }
        public int? DayAbsence { get; set; }
        public long? MDelay { get; set; }
        public decimal? HExtraTime { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DeductionOther { get; set; }
        public decimal? ExtraTime { get; set; }
        public long? CreatedBy { get; set; }
        public decimal? AllowanceOther { get; set; }
        public decimal? DueDayWork { get; set; }
        public int? DayPrevMonth { get; set; }
        public decimal? DuePrevMonth { get; set; }
        public string? PackageNo { get; set; }
        public int? FacilityId { get; set; }
        public string? Note { get; set; }
        public decimal? Commission { get; set; }
        public int? PayrollTypeId { get; set; }
        public decimal? Penalties { get; set; }
        public string? DaysOfmonth { get; set; }
        public bool IsDeleted { get; set; }


    }
    public class HrPreparationSalaryEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }

        public long? DeptId { get; set; }
        public long? Location { get; set; }
        public int? FinancelYear { get; set; }
        public string? MsMonth { get; set; }
        public string? MsDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public int? CountDayWork { get; set; }
        public int? DayAbsence { get; set; }
        public long? MDelay { get; set; }
        public decimal? HExtraTime { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DeductionOther { get; set; }
        public decimal? ExtraTime { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? AllowanceOther { get; set; }
        public decimal? DueDayWork { get; set; }
        public int? DayPrevMonth { get; set; }
        public decimal? DuePrevMonth { get; set; }
        public string? PackageNo { get; set; }
        public int? FacilityId { get; set; }
        public string? Note { get; set; }
        public decimal? Commission { get; set; }
        public int? PayrollTypeId { get; set; }
        public decimal? Penalties { get; set; }
    }


    public class HrPreparationSalariesFilterDto
    {
        public string EmpCode { get; set; } = null!;
        public long Id { get; set; }
        public long? EmpId { get; set; }

        public long? DeptId { get; set; }
        public int? FinancelYear { get; set; }
        public string? MsMonth { get; set; }
        public long? Location { get; set; }
        public string? DepName { get; set; }
        public string? LocationName { get; set; }

        public string? EmpName { get; set; }
        public decimal? NetSalary { get; set; }
    }
    public class HrPreparationSalariesGetByIdDto
    {
        public HrPreparationSalariesDataDto? SalaryData { get; set; }
        public List<HrPsAllowanceDeductionVw>? allowance { get; set; }
        public List<HrPsAllowanceDeductionVw>? deduction { get; set; }
    }
    public class HrPreparationSalariesDataDto
    {
        public string? EmpCode { get; set; }
        public long Id { get; set; }
        public long? EmpId { get; set; }

        public long? DeptId { get; set; }
        public int? FinancelYear { get; set; }
        public string? MsMonth { get; set; }
        public long? Location { get; set; }
        public string? DepName { get; set; }
        public string? LocationName { get; set; }

        public string? EmpName { get; set; }
        public decimal? NetSalary { get; set; }
        public string? MsDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public int? CountDayWork { get; set; }
        public int? DayAbsence { get; set; }
        public long? MDelay { get; set; }
        public decimal? HExtraTime { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DeductionOther { get; set; }
        public decimal? ExtraTime { get; set; }
        public decimal? AllowanceOther { get; set; }
        public int? BranchId { get; set; }
        public string? BankName { get; set; }
        public int? BankId { get; set; }
        public string? AccountNo { get; set; }
        public decimal? DailyWorkingHours { get; set; }

        public decimal? BasicSalary { get; set; }
        public decimal? DueDayWork { get; set; }
        public int? DayPrevMonth { get; set; }
        public decimal? DuePrevMonth { get; set; }
        public string? PackageNo { get; set; }
        public string? UserFullname { get; set; }
        public int? FacilityId { get; set; }
        public string? Iban { get; set; }

        public string? NationalityName { get; set; }

        public string? CatName { get; set; }
        public int? Expr1 { get; set; }
        public string? Note { get; set; }
        public decimal? Commission { get; set; }
        public int? PayrollTypeId { get; set; }
        public decimal? Penalties { get; set; }
        public int? NationalityId { get; set; }
        public string? DaysOfmonth { get; set; }
        public int? salaryMethod { get; set; }
    }

    public class AddPreparationSalariesResultDto
    {
        public string? IsExist { get; set; }
        public string? EmpNotExists { get; set; }
        public string? IDNotEqual { get; set; }
        public string? NotActive { get; set; }
        public string? Saved { get; set; }

    }

    public class AddPreparationSalariesDto
    {
        public HrEmployeeVw? HrEmployee { get; set; }

        public decimal allowanceAmount { get; set; }
        public decimal deductionAmount { get; set; }
        public decimal totalSalary { get; set; }
        public int countDayWork { get; set; }

    }

    public class HrPreparationCommissionUpdateDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? FinancelYear { get; set; }
        public string? MsMonth { get; set; }
        public string? MsDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public int? CountDayWork { get; set; }
        public int? DayAbsence { get; set; }
        public long? MDelay { get; set; }
    }
    [Keyless]
    public class HRPreparationSalariesLoanDto
    {
        public string? Emp_Code { get; set; }
        public string? Emp_name { get; set; }
        public int? Emp_ID { get; set; }
        public int? ID { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? Commission { get; set; }
        public int? OverTime { get; set; }
        public decimal? Loan { get; set; }
        public decimal? LoanBalance { get; set; }
    }

    public class HRPreparationSalariesLoanFilterDto
    {

            public int? FinancelYear { get; set; }
            public string? MSMonth { get; set; }
            public int? FinYear { get; set; }
            public int? FacilityID { get; set; }
            public int? DeptID { get; set; }
            public int? Location { get; set; }
            public int ?BranchID { get; set; }
            public int ?CMDType { get; set; }
        }

    public class HRPreparationSalariesLoanAddDto
    {
        public long ID { get; set; }

        public decimal? Loan { get; set; }
    }
}
