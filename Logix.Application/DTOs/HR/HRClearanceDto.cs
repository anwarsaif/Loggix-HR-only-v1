using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public partial class HrClearanceDto
    {
        public long Id { get; set; }
        public string? DateC { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? ClearanceType { get; set; }
        public long? BasicSalary { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalSalary { get; set; }
        public string? LastVacationDate { get; set; }
        public int? LastVacationType { get; set; }
        public int? VacationDaysYear { get; set; }
        public long? LocationId { get; set; }
        public long? DepId { get; set; }
        public string? BankId { get; set; }
        public string? Iban { get; set; }
        public string? LastSalaryDate { get; set; }
        public string? VacationSdate { get; set; }
        public string? VacationEdate { get; set; }
        public int? VacationAccountDay { get; set; }
        public int? VacationDayWithSalary { get; set; }
        public int? VacationDayWithoutSalary { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public decimal? SalaryC { get; set; }
        public decimal? HousingC { get; set; }
        public decimal? AllowanceC { get; set; }
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        public decimal? DayClearanceAmount { get; set; }
        public decimal? DayClearance { get; set; }
        public decimal? TickDueTotal { get; set; }
        public int? TickDueCnt { get; set; }
        public decimal? TickDueAmount { get; set; }
        public decimal? TotalAllowance { get; set; }
        public decimal? DedHousing { get; set; }
        public decimal? Loan { get; set; }
        public decimal? Gosi { get; set; }
        public string? GosiNote { get; set; }
        public decimal? DedOhad { get; set; }
        public string? DedOhadNote { get; set; }
        public decimal? Delay { get; set; }
        public int? DelayCnt { get; set; }
        public decimal? Absence { get; set; }
        public int? AbsenceCnt { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? TotalDeduction { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? PayrollId { get; set; }
        public int? CountDayWork { get; set; }
        public long? VacationId { get; set; }
        public string? LastWorkingDay { get; set; }
        public decimal? OtherDeduction { get; set; }
        public string? OtherDeductionNote { get; set; }
        public long? BranchId { get; set; }
        public string? LastVacationEdate { get; set; }
    }

    public partial class HrClearanceEditDto
    {
        public long Id { get; set; }
        [Required]
        public int? ClearanceType { get; set; }
        //  تاريخ المخالصة 
        [Required]
        public string? LeaveDate { get; set; }
        [Required]
        public string? LastWorkingDay { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public long? BasicSalary { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalSalary { get; set; }
        public string? LastVacationDate { get; set; }
        public int? LastVacationType { get; set; }
        public int? VacationDaysYear { get; set; }
        public string? LastVacationEdate { get; set; }
        public string? Iban { get; set; }
        public string? LastSalaryDate { get; set; }
        [Required]
        public string? VacationSdate { get; set; }
        [Required]
        public string? VacationEdate { get; set; }
        [Required]
        public int? VacationAccountDay { get; set; }
        [Required]
        public int? VacationDayWithSalary { get; set; }
        [Required]
        public int? VacationDayWithoutSalary { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public int? CountDayWork { get; set; }
        //
        [Required]
        public decimal? SalaryC { get; set; }
        [Required]
        public decimal? HousingC { get; set; }
        public decimal? AllowanceC { get; set; }
        public decimal? DayClearanceAmount { get; set; }
        [Required]
        public decimal? DayClearance { get; set; }
        [Required]
        public decimal? TickDueTotal { get; set; }
        public int? TickDueCnt { get; set; }
        public decimal? TickDueAmount { get; set; }
        [Required]
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        [Required]
        public decimal? TotalAllowance { get; set; }
        [Required]
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        //
        [Required]
        public decimal? DedHousing { get; set; }
        [Required]
        public decimal? Loan { get; set; }
        [Required]
        public decimal? Gosi { get; set; }
        public string? GosiNote { get; set; }
        [Required]
        public decimal? DedOhad { get; set; }
        public string? DedOhadNote { get; set; }
        [Required]
        public decimal? Delay { get; set; }
        public int? DelayCnt { get; set; }
        [Required]
        public decimal? Absence { get; set; }
        public int? AbsenceCnt { get; set; }
        [Required]
        public decimal? OtherDeduction { get; set; }
        public string? OtherDeductionNote { get; set; }
        [Required]
        public decimal? Penalties { get; set; }
        [Required]
        public decimal? TotalDeduction { get; set; }
        //
        public long? DepId { get; set; }
        public long? LocationId { get; set; }
        public long? BranchId { get; set; }

        public List<HrAllowancesVm>? AllowancesList { get; set; }
    }

    public partial class HrClearanceFilterDto
    {
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public long? BranchId { get; set; }
        public long? LocationId { get; set; }
        public long? DeptId { get; set; }
        public int? ClearanceType { get; set; }

        ////////////////////////////////////////////////////////////
        public decimal? DueAmount { get; set; }
        public string? Tdate { get; set; }
        public string? VacationDateFrom { get; set; }
        public string? VacationDateTo { get; set; }
        public long? ReferenceNo { get; set; }
        public string? BranchName { get; set; }
        public string? LocationName { get; set; }
        public string? DeptName { get; set; }
    }

    public partial class HrClearanceAddDto
    {
        public long Id { get; set; }
        [Required]
        public int? ClearanceType { get; set; }
        //  تاريخ المخالصة 
        [Required]
        public string? LeaveDate { get; set; }
        [Required]
        public string? LastWorkingDay { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public long? BasicSalary { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalSalary { get; set; }
        public string? LastVacationDate { get; set; }
        public int? LastVacationType { get; set; }
        public int? VacationDaysYear { get; set; }
        public string? LastVacationEdate { get; set; }
        public string? Iban { get; set; }
        public string? LastSalaryDate { get; set; }
        [Required]
        public string? VacationSdate { get; set; }
        [Required]
        public string? VacationEdate { get; set; }
        [Required]
        public int? VacationAccountDay { get; set; }
        [Required]
        public int? VacationDayWithSalary { get; set; }
        [Required]
        public int? VacationDayWithoutSalary { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public int? CountDayWork { get; set; }
        //
        [Required]
        public decimal? SalaryC { get; set; }
        [Required]
        public decimal? HousingC { get; set; }
        public decimal? AllowanceC { get; set; }
        public decimal? DayClearanceAmount { get; set; }
        [Required]
        public decimal? DayClearance { get; set; }
        [Required]
        public decimal? TickDueTotal { get; set; }
        public int? TickDueCnt { get; set; }
        public decimal? TickDueAmount { get; set; }
        [Required]
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        [Required]
        public decimal? TotalAllowance { get; set; }
        [Required]
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public bool? CreateBalance { get; set; } //انشاء رصيد افتتاحي جديد
        //
        [Required]
        public decimal? DedHousing { get; set; }
        [Required]
        public decimal? Loan { get; set; }
        [Required]
        public decimal? Gosi { get; set; }
        public string? GosiNote { get; set; }
        [Required]
        public decimal? DedOhad { get; set; }
        public string? DedOhadNote { get; set; }
        [Required]
        public decimal? Delay { get; set; }
        public int? DelayCnt { get; set; }
        [Required]
        public decimal? Absence { get; set; }
        public int? AbsenceCnt { get; set; }
        [Required]
        public decimal? OtherDeduction { get; set; }
        public string? OtherDeductionNote { get; set; }
        [Required]
        public decimal? Penalties { get; set; }
        [Required]
        public decimal? TotalDeduction { get; set; }
        // if create balance chkbox is checked
        public string? StartBalanceDate { get; set; }
        public decimal? Remainingbalance { get; set; }
        public string? CreateBalanceNote { get; set; }
        //
        public long? VacationId { get; set; }
        public long? DepId { get; set; }
        public long? LocationId { get; set; }
        public long? BranchId { get; set; }

        public List<HrAllowancesVm>? AllowancesList { get; set; }
    }

    public class HrAllowancesVm
    {
        public long? Id { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string? Name { get; set; }
        public decimal? NewAmount { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }

    public class HrClearancePayrollTransferDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public string? ClearanceDate { get; set; }
        // صافي الاستحقاقات
        public decimal? Net { get; set; }
        public int? AppTypeId { get; set; } = 0;
        public int? BankId { get; set; }
    }

    public partial class HrClearanceAddDto2
    {
        public long Id { get; set; }
        [Required]
        public int? ClearanceType { get; set; }
        //  تاريخ المخالصة 
        [Required]
        public string? LeaveDate { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public long? BasicSalary { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalSalary { get; set; }
        public string? LastVacationDate { get; set; }
        public int? LastVacationType { get; set; }
        public int? VacationDaysYear { get; set; }
        public string? Iban { get; set; }
        public string? LastSalaryDate { get; set; }
        public string? VacationSdate { get; set; }
        public string? VacationEdate { get; set; }
        public int? VacationAccountDay { get; set; }
        public int? VacationDayWithSalary { get; set; }
        public int? VacationDayWithoutSalary { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public decimal? DayClearanceAmount { get; set; }
        [Required]
        public decimal? DayClearance { get; set; }
        [Required]
        public decimal? TickDueTotal { get; set; }
        public int? TickDueCnt { get; set; }
        public decimal? TickDueAmount { get; set; }
        [Required]
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        [Required]
        public decimal? TotalAllowance { get; set; }
        [Required]
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        [Required]
        public decimal? DedOhad { get; set; }
        public string? DedOhadNote { get; set; }
        [Required]
        public decimal? TotalDeduction { get; set; }

        public List<HrClearanceMonthVm>? ClearanceMonthList { get; set; }
    }
}
