using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

    [Keyless]
    public class HrLeaveVw : TraceEntity
    {
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;

        [Column("ID")]
        public long Id { get; set; }

        [Column("Leave_Date")]
        [StringLength(50)]
        public string? LeaveDate { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        public int? WorkYear { get; set; }

        public int? WorkMonth { get; set; }

        public int? WorkDays { get; set; }

        [Column("Leave_Type")]
        public int? LeaveType { get; set; }

        [Column("Basic_Salary")]
        public long? BasicSalary { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Housing { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Allowances { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deduction { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalSalary { get; set; }

        [Column("Last_Vacation_Date")]
        [StringLength(10)]
        public string? LastVacationDate { get; set; }

        [Column("Last_Vacation_Type")]
        public int? LastVacationType { get; set; }

        [Column("Vacation_Days_Year")]
        public int? VacationDaysYear { get; set; }

        [Column("Location_ID")]
        public long? LocationId { get; set; }

        [Column("Dep_ID")]
        public long? DepId { get; set; }

        [Column("Bank_ID")]
        [StringLength(10)]
        public string? BankId { get; set; }

        [StringLength(50)]
        public string? Iban { get; set; }

        [Column("Last_Salary_Date")]
        [StringLength(10)]
        public string? LastSalaryDate { get; set; }

        [Column("Salary_C", TypeName = "decimal(18, 2)")]
        public decimal? SalaryC { get; set; }

        [Column("Housing_C", TypeName = "decimal(18, 2)")]
        public decimal? HousingC { get; set; }

        [Column("Allowance_C", TypeName = "decimal(18, 2)")]
        public decimal? AllowanceC { get; set; }

        [Column("Other_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? OtherAllowance { get; set; }

        [Column("Other_Allowance_Note")]
        public string? OtherAllowanceNote { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? VacationBalance { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? VacationBalanceAmount { get; set; }

        [Column("End_service_benefits", TypeName = "decimal(18, 2)")]
        public decimal? EndServiceBenefits { get; set; }

        [Column("End_service_indemnity", TypeName = "decimal(18, 2)")]
        public decimal? EndServiceIndemnity { get; set; }

        [Column("End_service_indemnity_Note")]
        public string? EndServiceIndemnityNote { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Bounce { get; set; }

        [Column("Bounce_Note")]
        public string? BounceNote { get; set; }

        [Column("Tick_Due_Total", TypeName = "decimal(18, 2)")]
        public decimal? TickDueTotal { get; set; }

        [Column("Tick_Due_Cnt")]
        public int? TickDueCnt { get; set; }

        [Column("Tick_Due_Amount", TypeName = "decimal(18, 2)")]
        public decimal? TickDueAmount { get; set; }

        [Column("Total_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? TotalAllowance { get; set; }

        [Column("Ded_Housing", TypeName = "decimal(18, 2)")]
        public decimal? DedHousing { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Loan { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Gosi { get; set; }

        [Column("Gosi_Note")]
        [StringLength(50)]
        public string? GosiNote { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DedOhad { get; set; }

        [Column("DedOhad_Note")]
        public string? DedOhadNote { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Delay { get; set; }

        [Column("Delay_Cnt")]
        public int? DelayCnt { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Absence { get; set; }

        [Column("Absence_Cnt")]
        public int? AbsenceCnt { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Penalties { get; set; }

        [Column("MD_Insurance", TypeName = "decimal(18, 2)")]
        public decimal? MdInsurance { get; set; }

        [Column("MD_Insurance_Note")]
        public string? MdInsuranceNote { get; set; }

        [Column("Other_Deduction", TypeName = "decimal(18, 2)")]
        public decimal? OtherDeduction { get; set; }

        [Column("Other_Deduction_Note")]
        public string? OtherDeductionNote { get; set; }

        [Column("Total_Deduction", TypeName = "decimal(18, 2)")]
        public decimal? TotalDeduction { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Net { get; set; }

        public string? Note { get; set; }

        [Column("Have_Bank_loan")]
        public bool? HaveBankLoan { get; set; }

        [Column("Type_name")]
        [StringLength(550)]
        public string? TypeName { get; set; }

        [Column("DOAppointment")]
        [StringLength(12)]
        public string? Doappointment { get; set; }

        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }

        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }

        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }

        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }

        [Column("Bank_Name")]
        [StringLength(250)]
        public string? BankName { get; set; }

        [Column("Dept_ID")]
        public int? DeptId { get; set; }

        public int? Location { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("Payroll_ID")]
        public long? PayrollId { get; set; }

        [Column("Count_Day_Work")]
        public int? CountDayWork { get; set; }

        public int? Expr1 { get; set; }

        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }

        [Column("BRA_NAME")]
        public string? BraName { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }

        [Column("Facility_Name")]
        [StringLength(500)]
        public string? FacilityName { get; set; }

        [Column("Leave_Type2")]
        public int? LeaveType2 { get; set; }

        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }

        [Column("Last_working_Day")]
        [StringLength(10)]
        public string? LastWorkingDay { get; set; }

        public long? Expr2 { get; set; }

        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }

        [Column("Cat_name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }

        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }

        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }

        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }

        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }

        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }

        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }

        [Column("Type_name2")]
        [StringLength(550)]
        public string? TypeName2 { get; set; }

        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }

        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesId { get; set; }

        [Column("Leave_Type2_Name")]
        [StringLength(550)]
        public string? LeaveType2Name { get; set; }

        [Column("Leave_Type2_Name2")]
        [StringLength(550)]
        public string? LeaveType2Name2 { get; set; }

        [Column("App_ID")]
        public long? AppId { get; set; }

        [Column("Payroll_Code")]
        public long? PayrollCode { get; set; }

        [Column("ProvEndServes_Amount", TypeName = "decimal(18, 2)")]
        public decimal? ProvEndServesAmount { get; set; }

        [Column("Net_provision", TypeName = "decimal(18, 2)")]
        public decimal? NetProvision { get; set; }

        public bool? HaveCustody { get; set; }
    }
}
