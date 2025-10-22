using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]

    public class HrClearanceVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DedOhad { get; set; }
        [Column("DedOhad_Note")]
        public string? DedOhadNote { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Net { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("DOAppointmentold")]
        [StringLength(50)]
        public string? Doappointmentold { get; set; }
        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }
        [Column("DOAppointment")]
        [StringLength(12)]
        public string? Doappointment { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deduction { get; set; }
        [Column("Vacation_Account_Day")]
        public int? VacationAccountDay { get; set; }
        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }
        [Column("Vacation_Days_Year")]
        public int? VacationDaysYear { get; set; }
        [Column("Bank_Name")]
        [StringLength(250)]
        public string? BankName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public string? Note { get; set; }
        [Column("Total_Deduction", TypeName = "decimal(18, 2)")]
        public decimal? TotalDeduction { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Penalties { get; set; }
        [Column("Absence_Cnt")]
        public int? AbsenceCnt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Absence { get; set; }
        [Column("Delay_Cnt")]
        public int? DelayCnt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Delay { get; set; }
        [Column("Gosi_Note")]
        [StringLength(50)]
        public string? GosiNote { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Loan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Gosi { get; set; }
        [Column("Ded_Housing", TypeName = "decimal(18, 2)")]
        public decimal? DedHousing { get; set; }
        [Column("Tick_Due_Amount", TypeName = "decimal(18, 2)")]
        public decimal? TickDueAmount { get; set; }
        [Column("Total_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? TotalAllowance { get; set; }
        [Column("Date_C")]
        [StringLength(10)]
        public string? DateC { get; set; }
        [Column("Clearance_Type")]
        public int? ClearanceType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Housing { get; set; }
        [Column("Basic_Salary")]
        public long? BasicSalary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Allowances { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalSalary { get; set; }
        [Column("Last_Vacation_Date")]
        [StringLength(10)]
        public string? LastVacationDate { get; set; }
        [Column("Last_Vacation_Type")]
        public int? LastVacationType { get; set; }
        [Column("Dep_ID")]
        public long? DepId { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Bank_ID")]
        [StringLength(10)]
        public string? BankId { get; set; }
        [Column("Vacation_SDate")]
        [StringLength(10)]
        public string? VacationSdate { get; set; }
        [Column("Last_Salary_Date")]
        [StringLength(10)]
        public string? LastSalaryDate { get; set; }
        [Column("Vacation_EDate")]
        [StringLength(10)]
        public string? VacationEdate { get; set; }
        [Column("Vacation_Day_WithSalary")]
        public int? VacationDayWithSalary { get; set; }
        [Column("Vacation_Day_WithoutSalary")]
        public int? VacationDayWithoutSalary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? VacationBalanceAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? VacationBalance { get; set; }
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
        [Column("Day_Clearance", TypeName = "decimal(18, 2)")]
        public decimal? DayClearance { get; set; }
        [Column("Day_ClearanceAmount", TypeName = "decimal(18, 2)")]
        public decimal? DayClearanceAmount { get; set; }
        [Column("Tick_Due_Cnt")]
        public int? TickDueCnt { get; set; }
        [Column("Tick_Due_Total", TypeName = "decimal(18, 2)")]
        public decimal? TickDueTotal { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Type_name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? Expr1 { get; set; }
        [Column("Payroll_ID")]
        public long? PayrollId { get; set; }
        [Column("Count_Day_Work")]
        public int? CountDayWork { get; set; }
        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }
        [Column("Vacation_ID")]
        public long? VacationId { get; set; }
        [Column("Last_working_Day")]
        [StringLength(10)]
        public string? LastWorkingDay { get; set; }
        [Column("Other_Deduction", TypeName = "decimal(18, 2)")]
        public decimal? OtherDeduction { get; set; }
        [Column("Other_Deduction_Note")]
        public string? OtherDeductionNote { get; set; }
        [Column("Last_Vacation_EDate")]
        [StringLength(10)]
        public string? LastVacationEdate { get; set; }
        [Column("Last_Vacation_Type_Name")]
        [StringLength(500)]
        public string? LastVacationTypeName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Type_name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }
        [Column("Bank_Name2")]
        [StringLength(250)]
        public string? BankName2 { get; set; }
        [Column("Marital_Status_Name2")]
        [StringLength(250)]
        public string? MaritalStatusName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Cat_name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Last_Vacation_Type_Name2")]
        [StringLength(500)]
        public string? LastVacationTypeName2 { get; set; }
    }
}
