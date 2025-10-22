using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Logix.Application.DTOs.Main;

using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{
    public class HrLeaveDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string? LeaveDate { get; set; }
        public long? EmpId { get; set; }
        public int? WorkYear { get; set; }
        public int? WorkMonth { get; set; }
        public int? WorkDays { get; set; }
        public int? LeaveType { get; set; }
        public long? BasicSalary { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalSalary { get; set; }
        [StringLength(10)]
        public string? LastVacationDate { get; set; }
        public int? LastVacationType { get; set; }
        public int? VacationDaysYear { get; set; }
        public long? LocationId { get; set; }
        public long? DepId { get; set; }
        [StringLength(10)]
        public string? BankId { get; set; }
        [StringLength(50)]
        public string? Iban { get; set; }
        [StringLength(10)]
        public string? LastSalaryDate { get; set; }
        public decimal? SalaryC { get; set; }
        public decimal? HousingC { get; set; }
        public decimal? AllowanceC { get; set; }
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public decimal? EndServiceBenefits { get; set; }
        public decimal? EndServiceIndemnity { get; set; }
        public string? EndServiceIndemnityNote { get; set; }
        public int? Bounce { get; set; }
        public string? BounceNote { get; set; }
        public decimal? TickDueTotal { get; set; }
        public int? TickDueCnt { get; set; }
        public decimal? TickDueAmount { get; set; }
        public decimal? TotalAllowance { get; set; }
        public decimal? DedHousing { get; set; }
        public decimal? Loan { get; set; }
        public decimal? Gosi { get; set; }
        [StringLength(50)]
        public string? GosiNote { get; set; }
        public decimal? DedOhad { get; set; }
        public string? DedOhadNote { get; set; }
        public decimal? Delay { get; set; }
        public int? DelayCnt { get; set; }
        public decimal? Absence { get; set; }
        public int? AbsenceCnt { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? MdInsurance { get; set; }
        public string? MdInsuranceNote { get; set; }
        public decimal? OtherDeduction { get; set; }
        public string? OtherDeductionNote { get; set; }
        public decimal? TotalDeduction { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public bool? HaveBankLoan { get; set; }
        public long? PayrollId { get; set; }
        public int? CountDayWork { get; set; }
        public int? LeaveType2 { get; set; }
        [StringLength(10)]
        public string? LastWorkingDay { get; set; }
        public long? BranchId { get; set; }
        public long? AppId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrLeaveEditDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string? EmpCode { get; set; }

        public string? LeaveDate { get; set; }
        public int? WorkYear { get; set; }
        public int? WorkMonth { get; set; }
        public int? WorkDays { get; set; }
        public int? LeaveType { get; set; }
        public long? BasicSalary { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalSalary { get; set; }
        [StringLength(10)]
        public string? LastVacationDate { get; set; }
        public int? LastVacationType { get; set; }
        public int? VacationDaysYear { get; set; }

        [StringLength(50)]
        public string? Iban { get; set; }
        [StringLength(10)]
        public string? LastSalaryDate { get; set; }
        public decimal? SalaryC { get; set; }
        public decimal? HousingC { get; set; }
        public decimal? AllowanceC { get; set; }
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public decimal? EndServiceBenefits { get; set; }
        public decimal? EndServiceIndemnity { get; set; }
        public string? EndServiceIndemnityNote { get; set; }
        public decimal? Bounce { get; set; }
        public string? BounceNote { get; set; }
        public decimal? TickDueTotal { get; set; }
        public int? TickDueCnt { get; set; }
        public decimal? TickDueAmount { get; set; }
        public decimal? TotalAllowance { get; set; }
        public decimal? DedHousing { get; set; }
        public decimal? Loan { get; set; }
        public decimal? Gosi { get; set; }
        [StringLength(50)]
        public string? GosiNote { get; set; }
        public decimal? DedOhad { get; set; }
        public string? DedOhadNote { get; set; }
        public decimal? Delay { get; set; }
        public int? DelayCnt { get; set; }
        public decimal? Absence { get; set; }
        public int? AbsenceCnt { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? MdInsurance { get; set; }
        public string? MdInsuranceNote { get; set; }
        public decimal? OtherDeduction { get; set; }
        public string? OtherDeductionNote { get; set; }
        public decimal? TotalDeduction { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public bool? HaveBankLoan { get; set; }
        public int? CountDayWork { get; set; }
        public int? LeaveType2 { get; set; }
        [StringLength(10)]
        public string? LastWorkingDay { get; set; }
        public long? BranchId { get; set; }
        public long? AppId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        ////////////////////////////////////////
        public int? AppTypeId { get; set; } = 0;
        public long? MsCode { get; set; } = 0;

        public string? AccountNo { get; set; }
        public decimal? NetSalary { get; set; }
        public long? Salary { get; set; }
        public bool? HaveCustody { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrAllowanceVwDto>? allowanceList { get; set; }

    }
    public class HrLeaveFilterDto
    {
        public long? Id { get; set; }

        public string? EmpCode { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        public int? LeaveType { get; set; }
        //////////////////////
        public int? SortId { get; set; }
        public string? LocationName { get; set; }
        public string? DepName { get; set; }
        public string? BranchName { get; set; }
        public string? LeaveDate { get; set; }
        public int? WorkYear { get; set; }
        //  سبب انهاء الخدمة
        public string? TypeName { get; set; }
        public string? LastWorkingDay { get; set; }
        public long? PayrollCode { get; set; }


    }

    public class HrLeaveAddDto
    {
        public long Id { get; set; }
        public string? LeaveDate { get; set; }

        public int? WorkYear { get; set; }
        public int? WorkMonth { get; set; }
        public int? WorkDays { get; set; }
        public int? LeaveType { get; set; }
        public long? Salary { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalSalary { get; set; }
        public string? LastVacationDate { get; set; }
        public int? LastVacationType { get; set; }
        public int? VacationDaysYear { get; set; }
        public string? Iban { get; set; }
        public string? LastSalaryDate { get; set; }
        public decimal? SalaryC { get; set; }
        public decimal? HousingC { get; set; }
        public decimal? AllowanceC { get; set; }
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public decimal? EndServiceBenefits { get; set; }
        public decimal? EndServiceIndemnity { get; set; }
        public string? EndServiceIndemnityNote { get; set; }
        public decimal? Bounce { get; set; }
        public string? BounceNote { get; set; }
        public decimal? TickDueTotal { get; set; }
        public decimal? TickDueCnt { get; set; }
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
        public decimal? MdInsurance { get; set; }
        public string? MdInsuranceNote { get; set; }
        public decimal? OtherDeduction { get; set; }
        public string? OtherDeductionNote { get; set; }
        public decimal? TotalDeduction { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public bool? HaveBankLoan { get; set; }
        public long? PayrollId { get; set; }
        public int? CountDayWork { get; set; }
        public int? LeaveType2 { get; set; }
        public string? LastWorkingDay { get; set; }

        public long? AppId { get; set; }
        //-------------------------------------------
        public int? AppTypeId { get; set; } = 0;
        public string? EmpCode { get; set; }
        public bool CancelAccount { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrAllowanceVwDto>? allowanceList { get; set; }
        //-------------------------------------------
        public long? LocationId { get; set; }
        public long? DepId { get; set; }
        public long? BranchId { get; set; }
        public bool? HaveCustody { get; set; }
        public decimal? ProvEndServesAmount { get; set; }
        public decimal? NetProvision { get; set; }
        public decimal? NetSalary { get; set; }
    }

    public class HrLeaveGetDataDto
    {


        public int? DDLLeaveType { get; set; } = 0;
        public long? EmpId { get; set; }
        public int? DDLLeaveType2 { get; set; } = 0;
        public string? EmpCode { get; set; }
        public string? LastworkingDay { get; set; }


    }

    [Keyless]
    public class HrEmployeeLeaveResultDto : HrEmployeeVw
    {
        public int? MD_Insurance { get; set; }
        public decimal? Other_Deduction { get; set; }
        public decimal? Other_Allowance { get; set; }
        public int? Bounce { get; set; }
        public decimal? End_Service_Benefits { get; set; }
        public int? End_Service_Indemnity { get; set; }
        public int? WorkYear { get; set; }
        public int? WorkMonth { get; set; }
        public int? WorkDays { get; set; }
        public decimal? VacationBalance { get; set; }
        public int? Vacation_Type_Id { get; set; }
        public string? Last_Vacation_Date { get; set; }
        public string? Vacation_Account_Day { get; set; }
        public string? Vacation_Type_Name { get; set; }
        public string? Last_Salary_Date { get; set; }
        ///------------------------------------------------------------
        public decimal? Salary_C { get; set; }
        public decimal? Housing_C { get; set; }
        public decimal? Allowance_C { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DayCost4Clearnce { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        ///------------------------------------------------------------
        public int? Ded_Housing { get; set; }
        public int? DedOhad { get; set; }
        ///------------------------------------------------------------
        public decimal? Delay { get; set; }
        public decimal? Delay_Cnt { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Absence_Cnt { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? Gosi { get; set; }
        public decimal? Gosi_C { get; set; }
        public decimal? Tick_Due_Cnt { get; set; }
        public decimal? Tick_Due_Amount { get; set; }
        public decimal? Tick_Due_Total { get; set; }
        ///------------------------------------------------------------
        public int? Count_Day_Work { get; set; }
        public decimal? ProvEndServes_Amount { get; set; }
    }

    public class HrLeaveDashboardFilterDto
    {
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public int? LeaveType { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? Cnt { get; set; }
        public string? LeaveTypeName { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public string? Color { get; set; }
        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }
        public string? DepName { get; set; }
        public string? DepName2 { get; set; }
    }

    public partial class HrLeaveGetByIdDto
    {
        public string? EmpName { get; set; }
        public string EmpCode { get; set; } = null!;
        public long Id { get; set; }
        public string? LeaveDate { get; set; }
        public long? EmpId { get; set; }
        public int? WorkYear { get; set; }
        public int? WorkMonth { get; set; }
        public int? WorkDays { get; set; }
        public int? LeaveType { get; set; }
        public decimal? BasicSalary { get; set; }
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
        public decimal? SalaryC { get; set; }
        public decimal? HousingC { get; set; }
        public decimal? AllowanceC { get; set; }
        public decimal? OtherAllowance { get; set; }
        public string? OtherAllowanceNote { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public decimal? EndServiceBenefits { get; set; }
        public decimal? EndServiceIndemnity { get; set; }
        public string? EndServiceIndemnityNote { get; set; }
        public decimal? Bounce { get; set; }
        public string? BounceNote { get; set; }
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
        public decimal? MdInsurance { get; set; }
        public string? MdInsuranceNote { get; set; }
        public decimal? OtherDeduction { get; set; }
        public string? OtherDeductionNote { get; set; }
        public decimal? TotalDeduction { get; set; }
        public decimal? Net { get; set; }
        public string? Note { get; set; }
        public bool? HaveBankLoan { get; set; }
        public string? TypeName { get; set; }
        public string? Doappointment { get; set; }
        public string? NationalityName { get; set; }
        public string? CatName { get; set; }
        public string? LocationName { get; set; }
        public string? DepName { get; set; }
        public string? BankName { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public long? PayrollId { get; set; }
        public int? CountDayWork { get; set; }
        public int? Expr1 { get; set; }
        public string? AccountNo { get; set; }
        public string? BraName { get; set; }
        public int? FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public int? LeaveType2 { get; set; }
        public string? IdNo { get; set; }
        public string? LastWorkingDay { get; set; }
        public long? Expr2 { get; set; }
        public string? EmpName2 { get; set; }
        public string? CatName2 { get; set; }
        public string? QualificationName2 { get; set; }
        public string? FacilityName2 { get; set; }
        public string? NationalityName2 { get; set; }
        public string? LocationName2 { get; set; }
        public string? DepName2 { get; set; }
        public string? BraName2 { get; set; }
        public string? TypeName2 { get; set; }
        public int? NationalityId { get; set; }
        public int? JobCatagoriesId { get; set; }
        public string? LeaveType2Name { get; set; }
        public string? LeaveType2Name2 { get; set; }
        public long? AppId { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? ProvEndServesAmount { get; set; }
        public decimal? NetProvision { get; set; }
        public bool? HaveCustody { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrLeaveAllowanceVwDto>? HrLeaveAllowanceVwDto { get; set; }

    }

    public class HrLeavePayrollTransferDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BankId { get; set; }
        public decimal? Net { get; set; }
        public decimal SalaryC { get; set; }
        public decimal? TotalAllowance { get; set; }
        public decimal? TotalDeduction { get; set; }
        public string? CountDayWork { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public string? AccountNo { get; set; }
        public decimal? Loan { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? HousingC { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public decimal? TickDueTotal { get; set; }
        public string? EndServiceBenefits { get; set; }
        public string? EndServiceIndemnity { get; set; }
        public decimal? AllowanceC { get; set; }

        public decimal? Gosi { get; set; }
        public decimal? DedHousing { get; set; }
        public decimal? Housing { get; set; }
        public string? DedOhad { get; set; }
        public string? MdInsurance { get; set; }
        public string? OtherDeduction { get; set; }
        public string? ReferenceNo { get; set; }
    }

}
