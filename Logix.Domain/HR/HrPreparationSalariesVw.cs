using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPreparationSalariesVw
    {
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Dept_ID")]
        public long? DeptId { get; set; }
        public int? FinancelYear { get; set; }
        [Column("MS_Month")]
        [StringLength(2)]
        public string? MsMonth { get; set; }
        [Column("MS_Date")]
        [StringLength(10)]
        public string? MsDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        [Column("allowance", TypeName = "decimal(18, 2)")]
        public decimal? Allowance { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deduction { get; set; }
        [Column("Count_Day_Work")]
        public int? CountDayWork { get; set; }
        [Column("Day_Absence")]
        public int? DayAbsence { get; set; }
        [Column("M_Delay")]
        public long? MDelay { get; set; }
        [Column("H_Extra_time", TypeName = "decimal(18, 2)")]
        public decimal? HExtraTime { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Absence { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Delay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Loan { get; set; }
        [Column("Deduction_Other", TypeName = "decimal(18, 2)")]
        public decimal? DeductionOther { get; set; }
        [Column("Extra_time", TypeName = "decimal(18, 2)")]
        public decimal? ExtraTime { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("allowance_Other", TypeName = "decimal(18, 2)")]
        public decimal? AllowanceOther { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Bank_Name")]
        [StringLength(250)]
        public string? BankName { get; set; }
        [Column("Bank_ID")]
        public int? BankId { get; set; }
        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }
        [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
        public decimal? DailyWorkingHours { get; set; }
        public long? Location { get; set; }
        [Column("Dep_name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BasicSalary { get; set; }
        [Column("Due_Day_Work", TypeName = "decimal(18, 2)")]
        public decimal? DueDayWork { get; set; }
        [Column("Day_PrevMonth")]
        public int? DayPrevMonth { get; set; }
        [Column("Due_PrevMonth", TypeName = "decimal(18, 2)")]
        public decimal? DuePrevMonth { get; set; }
        [Column("Package_No")]
        [StringLength(50)]
        public string? PackageNo { get; set; }
        [Column("USER_FULLNAME")]
        [StringLength(50)]
        public string? UserFullname { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }
        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }
        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }
        public int? Expr1 { get; set; }
        public string? Note { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Commission { get; set; }
        [Column("Payroll_Type_ID")]
        public int? PayrollTypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Penalties { get; set; }
        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }
    }
}
