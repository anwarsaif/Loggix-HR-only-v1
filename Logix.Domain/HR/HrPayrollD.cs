using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Payroll_D")]
    [Index("MsId", Name = "NonClusteredIndex-20200318-154456")]
    public partial class HrPayrollD : TraceEntity
    {
        [Key]
        [Column("MSD_ID")]
        public long MsdId { get; set; }
        [Column("MS_ID")]
        public long MsId { get; set; }
        [Column("Job_ID")]
        [StringLength(10)]
        [Unicode(false)]
        public string? JobId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        [Column("Emp_Account_No")]
        [StringLength(50)]
        public string? EmpAccountNo { get; set; }
        public int? BankId { get; set; }
        [Column("Count_Day_Work")]
        public int? CountDayWork { get; set; }
        [Column("allowance", TypeName = "decimal(18, 2)")]
        public decimal? Allowance { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deduction { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Absence { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Delay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Loan { get; set; }
        [Column("Transaction_Installment", TypeName = "decimal(18, 2)")]
        public decimal? TransactionInstallment { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Net { get; set; }
        [Column(TypeName = "ntext")]
        public string? Note { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Commission { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OverTime { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Mandate { get; set; }
        [Column("H_OverTime", TypeName = "decimal(18, 2)")]
        public decimal? HOverTime { get; set; }
        [Column("H_Delay", TypeName = "decimal(18, 2)")]
        public decimal? HDelay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Penalties { get; set; }
        [Column("Salary_Orignal", TypeName = "decimal(18, 2)")]
        public decimal? SalaryOrignal { get; set; }
        [Column("Refrance_No")]
        public long? RefranceNo { get; set; }
        [Column("Dept_ID")]
        public long? DeptId { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Salary_Group_ID")]
        public long? SalaryGroupId { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column("Cnt_Absence")]
        public int? CntAbsence { get; set; }
        [Column("Payment_Type_ID")]
        public int? PaymentTypeId { get; set; }
        [Column("Wages_Protection")]
        public int? WagesProtection { get; set; }
        [Column("allowance_Orignal", TypeName = "decimal(18, 2)")]
        public decimal? AllowanceOrignal { get; set; }
        [Column("Deduction_Orignal", TypeName = "decimal(18, 2)")]
        public decimal? DeductionOrignal { get; set; }
        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }
    }
}
